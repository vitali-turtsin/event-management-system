using BLL.Contracts.Base.Services;
using BLL.DTO;
using DAL.DTO.Search;

namespace BLL.Contracts.App.Services;

public interface IEventService : IBaseService<Event, DAL.DTO.Event, Guid, EventSearch>;