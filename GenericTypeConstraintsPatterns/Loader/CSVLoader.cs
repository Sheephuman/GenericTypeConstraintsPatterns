using GenericTypeConstraintsPatterns.Entity;
using GenericTypeConstraintsPatterns.Interface;
using GenericTypeConstraintsPatterns.Repository;

namespace GenericTypeConstraintsPatterns.Loader
{
    public sealed class CsvLoader<TEntity> : ICsvLoader<TEntity>
        where TEntity : ICsvReadable<TEntity>
    {
        public IEnumerable<UserEntity> CsvLoad(string filePath)
        {
            var repository = new CsvRepository<UserEntity>(filePath, line =>
            {
                var userEntity = new UserEntity
                {
                    Id = int.Parse(line[0]),
                    DisplayName = line[1],
                    affiliation = line[2]
                };
                return userEntity;
            });
            return repository.LoadFromCsv();
        }
    }

}
