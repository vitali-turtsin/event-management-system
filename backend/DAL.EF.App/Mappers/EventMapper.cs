using Contracts.Base;
using DAL.DTO;

namespace DAL.EF.App.Mappers;

public class EventMapper(AutoMapper.IMapper mapper) : BaseMapper<Event, Domain.App.Event>(mapper);