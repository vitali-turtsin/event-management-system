using DAL.Contracts.Base.Repositories;
using DAL.DTO;
using DAL.DTO.Search;

namespace DAL.Contracts.App.Repositories;

public interface IEventRepository : IBaseRepository<Event, Guid, EventSearch>,
    IEventRepositoryCustom<Event>;

public interface IEventRepositoryCustom<TEntity>;