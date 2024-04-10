using BLL.DTO;
using Contracts.Base;

namespace BLL.App.Mappers;

public class OrganizationMapper(AutoMapper.IMapper mapper) : BaseMapper<Organization, DAL.DTO.Organization>(mapper);