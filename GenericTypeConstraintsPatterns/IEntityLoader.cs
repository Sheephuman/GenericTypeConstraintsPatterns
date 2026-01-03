using GenericTypeConstraintsPatterns.Entity;

namespace GenericTypeConstraintsPatterns
{
    public interface ICsvLoader<TEntity>
    {
        IEnumerable<UserEntity> CsvLoad(string filePath);
     
    }
}
