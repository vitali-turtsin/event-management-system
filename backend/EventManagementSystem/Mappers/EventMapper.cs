using Contracts.Base;
using PublicApi.v1.DTO;

namespace EventManagementSystem.Mappers;

public class EventMapper(AutoMapper.IMapper mapper) : BaseMapper<Event, BLL.DTO.Event>(mapper);