using BLL.Contracts.Base.Services;
using BLL.DTO;
using DAL.DTO.Search;

namespace BLL.Contracts.App.Services;

public interface IOrganizationService : IBaseService<Organization, DAL.DTO.Organization, Guid, OrganizationSearch>;