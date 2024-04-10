using Contracts.Base;
using DAL.Contracts.Base.Repositories;
using Domain.Contracts.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Base.Repositories;

public class BaseRepository<TDalEntity, TDomainEntity, TDbContext> :
    BaseRepository<TDalEntity, TDomainEntity, Guid, TDbContext, BaseSearch>,
    IBaseRepository<TDalEntity>
    where TDalEntity : class, IDomainEntityId
    where TDomainEntity : class, IDomainEntityId
    where TDbContext : DbContext
{
    protected BaseRepository(
        TDbContext dbContext,
        IMapper<TDalEntity, TDomainEntity> mapper) :
        base(dbContext, mapper)
    {
    }
}

public class BaseRepository<TDalEntity, TDomainEntity, TKey, TDbContext, TSearch>
    : IBaseRepository<TDalEntity, TKey, TSearch>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TDomainEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TDbContext : DbContext
    where TSearch : class, ISearch
{
    private readonly TDbContext _repoDbContext;
    protected readonly DbSet<TDomainEntity> RepoDbSet;
    protected readonly IMapper<TDalEntity, TDomainEntity> Mapper;

    protected BaseRepository(TDbContext dbContext, IMapper<TDalEntity, TDomainEntity> mapper)
    {
        _repoDbContext = dbContext;
        RepoDbSet = dbContext.Set<TDomainEntity>();
        Mapper = mapper;
    }

    protected virtual IQueryable<TDomainEntity> CreateQuery(bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();

        if (noTracking) query = query.AsNoTracking();

        return query;
    }

    public virtual async Task<IEnumerable<TDalEntity>> FindAllAsync(TSearch? search = default, bool noTracking = true)
    {
        search ??= Activator.CreateInstance<TSearch>();
        var query = CreateQuery(noTracking);

        query = query.OrderBy(entity => search.SortField);

        if (!search.IsAscending)
            query = query.Reverse();

        if (search.IsPagingEnabled)
            query = query.Skip(search.PageNumber * search.PageSize).Take(search.PageSize);

        return (await query.ToListAsync()).Select(Mapper.Map);
    }

    public virtual async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        return domainEntity == null ? null : Mapper.Map(domainEntity);
    }

    public virtual TDalEntity Add(TDalEntity entity)
    {
        if (entity is IDomainEntityMeta domainEntityMeta)
            domainEntityMeta.CreatedAt = DateTime.UtcNow;

        var addedDomainEntity = RepoDbSet.Add(Mapper.Map(entity)).Entity;

        return Mapper.Map(addedDomainEntity);
    }

    public virtual TDalEntity Update(TDalEntity entity)
    {
        if (entity is IDomainEntityMeta domainEntityMeta)
            domainEntityMeta.UpdatedAt = DateTime.UtcNow;

        var trackedEntity = _repoDbContext.ChangeTracker.Entries<TDomainEntity>()
            .FirstOrDefault(e => e.Entity.Id.Equals(entity.Id));

        if (trackedEntity != null) _repoDbContext.Entry(trackedEntity.Entity).State = EntityState.Detached;

        var updatedDomainEntity = RepoDbSet.Update(Mapper.Map(entity)).Entity;

        return Mapper.Map(updatedDomainEntity);
    }

    public virtual TDalEntity Remove(TDalEntity entity)
    {
        return Mapper.Map(RepoDbSet.Remove(Mapper.Map(entity)).Entity);
    }

    public virtual async Task<TDalEntity?> RemoveAsync(TKey id)
    {
        var entity = await FirstOrDefaultAsync(id, false);
        if (entity == null)
            throw new NullReferenceException($"Entity {typeof(TDalEntity).Name} with id {id} not found.");
        var removedEntity = Remove(entity);
        return await _repoDbContext.SaveChangesAsync() > 0 ? removedEntity : null;
    }

    public virtual async Task<bool> ExistsAsync(TKey id)
    {
        return await RepoDbSet.AnyAsync(e => e.Id.Equals(id));
    }
}