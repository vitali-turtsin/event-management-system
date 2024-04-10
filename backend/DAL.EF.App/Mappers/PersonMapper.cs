using Contracts.Base;
using DAL.DTO;

namespace DAL.EF.App.Mappers;

public class PersonMapper(AutoMapper.IMapper mapper) : BaseMapper<Person, Domain.App.Person>(mapper);