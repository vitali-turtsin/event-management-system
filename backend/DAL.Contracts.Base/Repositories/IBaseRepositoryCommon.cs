using Domain.Contracts.Base;

namespace DAL.Contracts.Base.Repositories;

public interface IBaseRepositoryCommon<TEntity, in TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    TEntity Add(TEntity entity);

    TEntity Update(TEntity entity);

    TEntity Remove(TEntity entity);
}