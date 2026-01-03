using GenericTypeConstraintsPatterns.Entity;
using GenericTypeConstraintsPatterns.Interface;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;


namespace GenericTypeConstraintsPatterns.ViewModel
{

    public sealed class MainViewModel : BindableBase
    {
        /// <summary>
        ///    /// <summary>
        /// 型 'TEntity' はジェネリック型またはメソッド 'IReadOnlyRepository<TEntity>' 内で型パラメーター 'TEntity' として使用できません。'TEntity' から 'GenericTypeConstraintsPatterns.Interface.IListDisplayEntity' へのボックス変換または型パラメーター変換がありません。 対応として以下の制約を追加

    


        public IListViewModel CurrentEntity
        {
            get
            {
                if (field == null)
                {
                    field = new ListViewModel<UserEntity>();

                    Debug.WriteLine(field.GetType());
                }
                    return field;
            }
            set
            {

                SetProperty(ref field, value);
            }
        }




        /// <summary>
        /// CS 9264 Non-nullable プロパティ 'FilePath' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier, or declaring the プロパティ as nullable, or safely handling the case where 'field' is null in the 'get' accessor. 
        /// 対応 NullCheckを追加
        /// </summary>
        public string FilePath
        {
            get
            {

                if (field == null)
                    throw new InvalidOperationException("field is null");

                return field;
            }
            set => SetProperty(ref field, value);
        }


        public ICommand ToggleEntityCommand { get; }

     
        public  ViewModelCommand? ViewModelCommands { get; }



        public MainViewModel()
        {
            if(ViewModelCommands is null)
                  ViewModelCommands = new ViewModelCommand(this);


            ToggleEntityCommand = new DelegateCommand(() =>
            {
                if (CurrentEntity is ListViewModel<UserEntity>)
                {
                    CurrentEntity = new ListViewModel<UserEntity>();
                }
                else
                {
                    CurrentEntity = new ListViewModel<LogEntity>();


                    /*
                     * CurrentEntity = new ListViewModel<LogEntity>();

                     型 'GenericTypeConstraintsPatterns.Entity.LogEntity' はジェネリック型またはメソッド 'ListViewModel<TEntity>' 内で型パラメーター 'TEntity' として使用できません。'GenericTypeConstraintsPatterns.Entity.LogEntity' から 'GenericTypeConstraintsPatterns.Interface.IListItem' への暗黙的な参照変換がありません。 
                     
                     */
                }
            });
        }

    
    }


}
