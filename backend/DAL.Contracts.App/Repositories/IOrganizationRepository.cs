using DAL.Contracts.Base.Repositories;
using DAL.DTO.Search;
using DAL.DTO;

namespace DAL.Contracts.App.Repositories;

public interface IOrganizationRepository : IBaseRepository<Organization, Guid, OrganizationSearch>,
    IOrganizationRepositoryCustom<Organization>;

public interface IOrganizationRepositoryCustom<TEntity>;