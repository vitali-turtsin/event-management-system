using DAL.Contracts.Base.Repositories;
using DAL.DTO.Search;
using DAL.DTO;

namespace DAL.Contracts.App.Repositories;

public interface IPersonRepository : IBaseRepository<Person, Guid, PersonSearch>,
    IPersonRepositoryCustom<Person>;

public interface IPersonRepositoryCustom<TEntity>;