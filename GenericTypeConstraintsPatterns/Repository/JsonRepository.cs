
using GenericTypeConstraintsPatterns.Interface;
using System.IO;
using System.Text.Json;

namespace GenericTypeConstraintsPatterns.Repository
{

    /*
     * この設計の強みは以下です：共通の保存・読み込みロジックを1箇所にまとめられるUserEntity 用、LogEntity 用と別々に JsonRepository を作らなくて済む
JSONファイルへのシリアライズ/デシリアライズ、ファイルパス管理、エラーハンドリングなどが全部共通化

型安全が保たれるwhere TEntity : class, IListItem のおかげで、IListItem を実装していない型はコンパイルエラーになる
誤って関係ないクラスをリポジトリに渡すミスを防げる


     */
    public class JsonRepository<TEntity> 
        where TEntity : class, ILogItem
    {
        private readonly string _filePath;
        readonly Func<string[], TEntity>? _factory;
        public JsonRepository(string filePath)
        {
            _filePath = filePath;
        }

       public JsonRepository(string filePath, Func<string[], TEntity> entityFactory)
        {
            _filePath = filePath;
            _factory = entityFactory;
        }

        public IEnumerable<TEntity> LoadAll()
        {
            try
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<TEntity>>(json)
                     
                    ?? Enumerable.Empty<TEntity>();
            }

            catch (Exception ex)
            {

                MessageBoxService.ShowError(ex, "Error reading JSON file");
                return Enumerable.Empty<TEntity>();
            }
        }
    }

}
