using BLL.Contracts.Base.Services;
using BLL.DTO;
using DAL.DTO.Search;

namespace BLL.Contracts.App.Services;

public interface IPersonService : IBaseService<Person, DAL.DTO.Person, Guid, PersonSearch>;