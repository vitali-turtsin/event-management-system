using BLL.DTO;
using Contracts.Base;

namespace BLL.App.Mappers;

public class PersonMapper(AutoMapper.IMapper mapper) : BaseMapper<Person, DAL.DTO.Person>(mapper);