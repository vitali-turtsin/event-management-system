using BLL.Contracts.Base.Services;
using Contracts.Base;
using DAL.Contracts.Base;
using DAL.Contracts.Base.Repositories;
using Domain.Contracts.Base;

namespace BLL.Base.Services;

public class BaseService<TUow, TRepository, TBllEntity, TDalEntity> :
    BaseService<TUow, TRepository, TBllEntity, TDalEntity, Guid, BaseSearch>,
    IBaseService<TBllEntity, TDalEntity>
    where TBllEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
    where TUow : IBaseUow
    where TRepository : IBaseRepository<TDalEntity, Guid, BaseSearch>
{
    protected BaseService(
        TUow serviceUow,
        TRepository serviceRepository,
        IMapper<TBllEntity, TDalEntity> mapper) :
        base(serviceUow, serviceRepository, mapper)
    {
    }
}

public class BaseService<TUow, TRepository, TBllEntity, TDalEntity, TKey, TSearch> :
    IBaseService<TBllEntity, TDalEntity, TKey, TSearch>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TUow : IBaseUow
    where TRepository : IBaseRepository<TDalEntity, TKey, TSearch>
    where TSearch : class, ISearch
{
    protected readonly TUow ServiceUow;
    protected TRepository ServiceRepository;
    protected readonly IMapper<TBllEntity, TDalEntity> Mapper;

    protected BaseService(
        TUow serviceUow,
        TRepository serviceRepository,
        IMapper<TBllEntity, TDalEntity> mapper)
    {
        ServiceUow = serviceUow;
        ServiceRepository = serviceRepository;
        Mapper = mapper;
    }

    public virtual async Task<IEnumerable<TBllEntity>> FindAllAsync(
        TSearch? search = default,
        bool noTracking = true)
    {
        return (await ServiceRepository.FindAllAsync(search, noTracking))
            .Select(Mapper.Map);
    }

    public virtual async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
    {
        var dalEntity = await ServiceRepository.FirstOrDefaultAsync(id, noTracking);
        return dalEntity == null ? null : Mapper.Map(dalEntity);
    }

    public virtual TBllEntity Add(TBllEntity entity)
    {
        return Mapper.Map(ServiceRepository.Add(Mapper.Map(entity)));
    }

    public virtual async Task<TBllEntity?> AddAsync(TBllEntity entity)
    {
        var addedBllEntity = Add(entity);
        await ServiceUow.SaveChangesAsync();
        return await FirstOrDefaultAsync(addedBllEntity.Id);
    }

    public virtual TBllEntity Update(TBllEntity entity)
    {
        return Mapper.Map(ServiceRepository.Update(Mapper.Map(entity)));
    }

    public virtual async Task<TBllEntity?> UpdateAsync(TBllEntity entity)
    {
        var bllEntity = await FirstOrDefaultAsync(entity.Id);
        if (bllEntity == null) return null;
        Mapper.Map(entity, bllEntity);
        var updatedBllEntity = Update(bllEntity);
        await ServiceUow.SaveChangesAsync();
        return await FirstOrDefaultAsync(updatedBllEntity.Id);
    }

    public virtual TBllEntity Remove(TBllEntity entity)
    {
        return Mapper.Map(ServiceRepository.Remove(Mapper.Map(entity)));
    }

    public virtual async Task<TBllEntity?> RemoveAsync(TKey id)
    {
        var bllEntity = await FirstOrDefaultAsync(id);
        if (bllEntity == null) return null;
        var removedDalEntity = Remove(bllEntity);
        return await ServiceUow.SaveChangesAsync() > 0 ? removedDalEntity : null;
    }

    public virtual async Task<bool> ExistsAsync(TKey id)
    {
        return await ServiceRepository.ExistsAsync(id);
    }
}