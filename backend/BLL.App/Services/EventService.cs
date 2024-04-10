using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using DAL.DTO.Search;
using BLL.DTO;
using BLL.Contracts.App.Services;
using DAL.Contracts.App;
using DAL.Contracts.App.Repositories;

namespace BLL.App.Services;

public class EventService(IAppUow serviceUow, IEventRepository serviceRepository, IMapper mapper)
    :
        BaseService<IAppUow, IEventRepository, Event, DAL.DTO.Event, Guid, EventSearch>(serviceUow, serviceRepository,
            new EventMapper(mapper)), IEventService;
