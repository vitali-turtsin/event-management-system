using Contracts.Base;
using Domain.Contracts.Base;

namespace DAL.Contracts.Base.Repositories;

public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, Guid, BaseSearch>
    where TEntity : class, IDomainEntityId;

public interface IBaseRepository<TEntity, TSearch> : IBaseRepository<TEntity, Guid, TSearch>
    where TEntity : class, IDomainEntityId
    where TSearch : class, ISearch;

public interface IBaseRepository<TEntity, TKey, TSearch> : IBaseRepositoryAsync<TEntity, TKey, TSearch>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TSearch : class, ISearch;