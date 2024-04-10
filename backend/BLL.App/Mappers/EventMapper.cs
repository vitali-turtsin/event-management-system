using BLL.DTO;
using Contracts.Base;

namespace BLL.App.Mappers;

public class EventMapper(AutoMapper.IMapper mapper) : BaseMapper<Event, DAL.DTO.Event>(mapper);