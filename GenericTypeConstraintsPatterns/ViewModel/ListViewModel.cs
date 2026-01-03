using GenericTypeConstraintsPatterns.Interface;
using Prism.Common;
using System.Collections.ObjectModel;

namespace GenericTypeConstraintsPatterns.ViewModel
    {

        public class ListViewModel<TEntity> : ObservableObject<TEntity>,IListViewModel
               where TEntity : class, new() // new() を追加すると Add コマンドで便利
    {


            ///// コレクション自体は固定（1つだけ作る）にして、内容をクリア＆追加
        public Type CurrentEntityType => typeof(TEntity);

        public ObservableCollection<object> Items { get; } = new ObservableCollection<object>();


        Type IListViewModel.CurrentEntityType { get => CurrentEntityType; set => throw new NotImplementedException(); }





        /// <summary>
        /// ListViewに通知するためのコレクション
        /// </summary>
        /// <param name="entities"></param>
        public ListViewModel(IEnumerable<TEntity>? entities = null)
            {
                Items = new ObservableCollection<object>();

                if (entities != null)
                {
                    foreach (var entity in entities)
                    {
                        Items.Add(entity);
                    }
                }
            }



        public void LoadDefault()
        {
            Items.Clear();
            Items.Add(new TEntity( ));
        }

        // オプション：後からデータを再読み込みしたい場合の便利メソッド
        public void LoadItems(IEnumerable<TEntity> entities)
            {
                Items.Clear();
                foreach (var entity in entities)
                {
                    Items.Add(entity);
                }
            }

            // オプション：新規追加コマンド用
            public void AddNewItem()
            {
                Items.Add(new TEntity());
            }
        }



        /*
      T ではなく TEntity
    where TEntity : IListItem で意図が一発で分かる
    MVVM 寄りだが責務は「表示用データ保持」だけ

        */
    }

