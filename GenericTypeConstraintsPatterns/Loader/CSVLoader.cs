using GenericTypeConstraintsPatterns.Entity;
using GenericTypeConstraintsPatterns.Interface;
using GenericTypeConstraintsPatterns.Repository;

namespace GenericTypeConstraintsPatterns.Loader
{

    /// <summary>
    /// 型 'TEntity' は、ジェネリック型のパラメーター 'TEntity'、またはメソッド 'CsvRepository<TEntity>' として使用するために、参照型でなければなりません　対応　制約の食い違いが原因
    /// →　where  TEntity : class, ICsvReadable<TEntity>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class CsvLoader<TEntity> : ICsvLoader<TEntity>
        where  TEntity : class, ICsvReadable<TEntity>
    {
        private readonly Func<string[], TEntity> _factory;

        public CsvLoader(Func<string[], TEntity> factory)
        {
            _factory = factory;
        }

        public IEnumerable<TEntity> CsvLoad(string filePath)
        {
            ///
            ////
            ///型 'TEntity' は、ジェネリック型のパラメーター 'TEntity'、またはメソッド 'CsvRepository<TEntity>' として使用するために、参照型でなければなりません
            ///→　CsvLoader<TEntity> の where に class を追加
            /// 
            ////


            var repository = new CsvRepository<TEntity>(filePath, _factory);
            return repository.LoadFromCsv();
        }
    }

}
