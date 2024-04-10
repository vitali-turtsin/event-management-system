using BLL.Contracts.Base;
using DAL.Contracts.Base;

namespace BLL.Base;

public class BaseBll<TUow> : IBaseBll
    where TUow : IBaseUow
{
    protected readonly TUow Uow;

    protected BaseBll(TUow uow)
    {
        Uow = uow;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await Uow.SaveChangesAsync();
    }

    private readonly Dictionary<Type, object> _serviceCache = [];

    public TService GetService<TService>(Func<TService> serviceCreationMethod) where TService : class
    {
        if (_serviceCache.TryGetValue(typeof(TService), out var repo))
            return (TService) repo;
            
        var repoInstance = serviceCreationMethod();
        _serviceCache.Add(typeof(TService), repoInstance);
        return repoInstance;
    }
}