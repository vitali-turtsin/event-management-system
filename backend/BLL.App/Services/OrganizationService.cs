using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using DAL.DTO.Search;
using BLL.DTO;
using BLL.Contracts.App.Services;
using DAL.Contracts.App.Repositories;
using DAL.Contracts.App;

namespace BLL.App.Services;

public class OrganizationService(IAppUow serviceUow, IOrganizationRepository serviceRepository, IMapper mapper)
    :
        BaseService<IAppUow, IOrganizationRepository, Organization, DAL.DTO.Organization, Guid, OrganizationSearch>(serviceUow, serviceRepository,
            new OrganizationMapper(mapper)), IOrganizationService;
