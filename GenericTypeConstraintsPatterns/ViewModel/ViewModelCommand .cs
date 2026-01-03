using GenericTypeConstraintsPatterns.Entity;
using GenericTypeConstraintsPatterns.Interface;
using GenericTypeConstraintsPatterns.Loader;
using GenericTypeConstraintsPatterns.Repository;
using Microsoft.Win32;
using System.Diagnostics;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace GenericTypeConstraintsPatterns.ViewModel
{

    /// <summary>
    /// MainViewModelから制御されるコマンド群
    /// </summary>
    public class ViewModelCommand : BindableBase
    {

        /// <summary>
        /// DelegateCommandはPrismのICommand実装
        /// なので実務上は抽象型を渡すのが正しい
        /// 汎用性を持たせるため
        /// </summary>
        public ICommand ShowInMemoryCommand { get; }
        public ICommand ShowCsvCommand { get; }
        public ICommand ShowJsonCommand { get; }
        public ICommand FilePickerCommand { get; set; }

        public ICommand ToggleEntityCommand { get; }


        public MainViewModel _mainViewModel { init; get; }
        ListViewModel<LogEntity> logEntity;
        ListViewModel<UserEntity> userEntity;

        public ViewModelCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            _mainViewModel.FilePath = string.Empty;
            ShowInMemoryCommand = new DelegateCommand(ShowInMemory);
            ShowCsvCommand = new DelegateCommand(ReadCsv<UserEntity>);
            ShowJsonCommand = new DelegateCommand(ShowJson<LogEntity>);
            FilePickerCommand = new DelegateCommand(FilePatheSelect);

             logEntity = new();
             userEntity = new();


            ToggleEntityCommand = new DelegateCommand(() =>
            {
                if (_mainViewModel.CurrentEntity.CurrentEntityType == typeof(UserEntity))
                    _mainViewModel.CurrentEntity = logEntity;
                else
                    _mainViewModel.CurrentEntity = userEntity;
                /*
                 * CurrentEntity = new ListViewModel<LogEntity>();

                 型 'GenericTypeConstraintsPatterns.Entity.LogEntity' はジェネリック型またはメソッド 'ListViewModel<TEntity>' 内で型パラメーター 'TEntity' として使用できません。'GenericTypeConstraintsPatterns.Entity.LogEntity' から 'GenericTypeConstraintsPatterns.Interface.IListItem' への暗黙的な参照変換がありません。 

                 */

            });

            ///System.InvalidOperationException: 'ItemsSource を使用する前に、Items コレクションが空である必要があります。 
            ///対応　ShowInMemory();　→ InitilizeComponent()の順
        }



        private void ReadCsv<TEntity>()
        {

            if (string.IsNullOrWhiteSpace(_mainViewModel.FilePath))
            {
                MessageBox.Show("CSVファイルが選択されていません。", "エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_mainViewModel.CurrentEntity.CurrentEntityType != typeof(UserEntity))
            {
                MessageBox.Show("CSV表示はUserEntityのみ対応しています。", "エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            logEntity.Items.Clear();

            ///CsvファイルのLoadの際、ラッパークラスをジェネリック化することで型安全に呼び出す実装　
            ///→　LogEntityでの呼び出しが出来なくなる。
            ///
            var csvloader = new CsvLoader<UserEntity>();
           
            ///null 参照の可能性があるものの逆参照です対応済み
            ///ifブロックで囲った
            if (_mainViewModel.CurrentEntity is ListViewModel<UserEntity> vm)
            {
                // vm は non-null
                vm.Items.Clear();


                foreach (var token in csvloader.CsvLoad(_mainViewModel.FilePath))
                    vm.Items.Add(token);
            }


            Debug.WriteLine(_mainViewModel.CurrentEntity.Items);
            foreach (var item in _mainViewModel.CurrentEntity.Items)
                Debug.WriteLine(item.GetType());
        }

        private void ShowInMemory()
        {


            var data = new[]
            {
            new UserEntity { Id = 1,DisplayName = "Alice", affiliation = "Common People" },
            new UserEntity { Id=2,DisplayName = "Bob",affiliation = "Regee Dancer" },
            new UserEntity { Id=3,DisplayName = "sheepMan", affiliation="Strange UMA" },
            new UserEntity { Id=4,DisplayName = "Molder", affiliation ="FBI Detective" },
            new UserEntity { Id=4,DisplayName = "Scully",affiliation ="FBI SheepLady Detective" }

        };

            //Bindingが壊れないよう、毎回Newするのをやめる
            //別Entityに切り換えるとBindingが破綻する
            //    Current = new ListViewModel<UserEntity>(data);


            if (_mainViewModel.CurrentEntity is ListViewModel<UserEntity> vm)
            {
                vm.Items.Clear();

                foreach (var e in data)
                {
                    vm.Items.Add(e);
                    Debug.WriteLine(vm.GetType());
                }

            }
           

        }
        
    

            
               



        /// <summary>
        /// 呼び出し時にILogItemフレームしか使えなくする
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        private void ShowJson<TEntity>() where TEntity : class, ILogItem, new()
        {
            if (string.IsNullOrWhiteSpace(_mainViewModel.FilePath))
            {
                MessageBox.Show("JSONファイルが選択されていません。", "エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            

            if (_mainViewModel.CurrentEntity.CurrentEntityType != typeof(LogEntity))
            {
                MessageBox.Show("Json表示は型情報：JsonEntityのみ対応しています。", "エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //既存Entityのクリア
            userEntity.Items.Clear();

            ///null 参照の可能性があるものの逆参照です対応済み
            ///ifブロックで囲った
            if (_mainViewModel.CurrentEntity is ListViewModel<LogEntity> vm)
            {
                // vm は non-null
                vm.Items.Clear();


            }

         


            try
            {
                /////GenericTypeConstraintsPatterns.Entity.UserEntity' から 'GenericTypeConstraintsPatterns.Interface.IListItem' への暗黙的な参照変換がありません。
                ///JsonRepository にIlogItemがないので追加

                var repository =
                    new JsonRepository<LogEntity>(_mainViewModel.FilePath);

                if (_mainViewModel.CurrentEntity is ListViewModel<LogEntity> logVm)
                {
                    logVm.Items.Clear();
                    foreach (var e in repository.LoadAll())
                        logVm.Items.Add(e);
                }
           

            }
            catch (JsonException ex)
            {
                MessageBoxService.ShowError(ex, ex.Message +"Error reading JSON file");
            }

        }
        

       

        public void FilePatheSelect()
        {
            // ダイアログのインスタンスを生成
            var dialog = new OpenFileDialog() {

                Filter = "CSVファイル (*.csv)|*.csv|JSONファイル (*.json)|*.json|すべてのファイル (*.*)|*.*",
            };
          
            // ファイルの種類を設定
            dialog.DefaultDirectory = "C\\";


            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                // 選択されたファイル名 (ファイルパス) をメッセージボックスに表示                
                _mainViewModel.FilePath = dialog.FileName;
            }
        }
    }

}
