using GenericTypeConstraintsPatterns.Entity;
using GenericTypeConstraintsPatterns.Interface;

namespace GenericTypeConstraintsPatterns
{
    public interface ICsvLoader<TEntity>
    where TEntity : class, ICsvReadable<TEntity>
    {
        IEnumerable<TEntity> CsvLoad(string filePath);
     
    }
}
