using Contracts.Base;
using PublicApi.v1.DTO;

namespace EventManagementSystem.Mappers;

public class OrganizationMapper(AutoMapper.IMapper mapper) : BaseMapper<Organization, BLL.DTO.Organization>(mapper);