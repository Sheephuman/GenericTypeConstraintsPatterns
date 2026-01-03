

namespace GenericTypeConstraintsPatterns.Entity
{
    public interface IJsonReadable<TEntity>     
    {
        IEnumerable<LogEntity> LoadFromJson();
    }
}
