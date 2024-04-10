using Contracts.Base;
using Domain.Contracts.Base;

namespace DAL.Contracts.Base.Repositories;

public interface IBaseRepositoryAsync<TEntity, in TKey, in TSearch> : IBaseRepositoryCommon<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TSearch : class, ISearch
{
    Task<IEnumerable<TEntity>> FindAllAsync(TSearch? search = default, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true);
    Task<bool> ExistsAsync(TKey id);
    Task<TEntity?> RemoveAsync(TKey id);
}