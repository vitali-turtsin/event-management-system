using Contracts.Base;
using PublicApi.v1.DTO;

namespace EventManagementSystem.Mappers;

public class PersonMapper(AutoMapper.IMapper mapper) : BaseMapper<Person, BLL.DTO.Person>(mapper);