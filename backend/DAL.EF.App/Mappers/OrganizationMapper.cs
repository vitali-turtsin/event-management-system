using Contracts.Base;
using DAL.DTO;

namespace DAL.EF.App.Mappers;

public class OrganizationMapper(AutoMapper.IMapper mapper) : BaseMapper<Organization, Domain.App.Organization>(mapper);