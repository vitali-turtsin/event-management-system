using Contracts.Base;
using DAL.Contracts.Base.Repositories;
using Domain.Contracts.Base;

namespace BLL.Contracts.Base.Services;

public interface IBaseService<TBllEntity, TDalEntity> :
    IBaseService<TBllEntity, TDalEntity, Guid, BaseSearch>
    where TBllEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId;

public interface IBaseService<TBllEntity, TDalEntity, TKey, TSearch> :
    IBaseRepository<TBllEntity, TKey, TSearch>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TSearch : class, ISearch;