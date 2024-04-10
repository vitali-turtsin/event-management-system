using DAL.Contracts.Base;

namespace DAL.Base;

public abstract class BaseUow : IBaseUow
{
    public abstract Task<int> SaveChangesAsync();

    private readonly Dictionary<Type, object> _repoCache = [];

    public TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
        where TRepository : class
    {
        if (_repoCache.TryGetValue(typeof(TRepository), out var repo))
        {
            return (TRepository) repo;
        }

        var repoInstance = repoCreationMethod();
        _repoCache.Add(typeof(TRepository), repoInstance);
        return repoInstance;
    }
}