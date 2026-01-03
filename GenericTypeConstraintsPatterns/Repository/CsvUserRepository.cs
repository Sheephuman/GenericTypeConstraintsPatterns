using GenericTypeConstraintsPatterns.Interface;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace GenericTypeConstraintsPatterns.Repository
{
    public class CsvRepository<TEntity> : ICsvReadable<TEntity>
        where TEntity : class, ICsvReadable<TEntity>
    {
        readonly string _filePath = string.Empty;
        readonly Func<string[], TEntity> _factory;

        public CsvRepository(string filePath, Func<string[], TEntity> entityFactory)
        {
            _filePath = filePath;
            _factory = entityFactory;
        }




        public ObservableCollection<TEntity> LoadFromCsv()
        {
            var collection = new ObservableCollection<TEntity>();

            if (string.IsNullOrWhiteSpace(_filePath))
            {
                MessageBox.Show("CSVファイルが選択されていません。", "エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
                return collection;
            }

            foreach (var line in File.ReadLines(_filePath))
            {
                var columns = line.Split(',');
                TEntity entity;
                try
                {
                    entity = _factory(columns);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error parsing CSV line", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }

                collection.Add(entity); // 追加ごとに UI に通知される
                int rowIndex = 0;
                foreach (var csvline in File.ReadLines(_filePath))
                {
                    rowIndex++;
                    var testcolumns = line.Split(',');
                    Debug.WriteLine($"Row {rowIndex}, ColumnCount: {testcolumns.Length}");
                }
            }

            return collection;
        }
    }
}
