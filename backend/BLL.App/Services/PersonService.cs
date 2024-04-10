using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using DAL.DTO.Search;
using BLL.DTO;
using BLL.Contracts.App.Services;
using DAL.Contracts.App.Repositories;
using DAL.Contracts.App;

namespace BLL.App.Services;

public class PersonService(IAppUow serviceUow, IPersonRepository serviceRepository, IMapper mapper)
    :
        BaseService<IAppUow, IPersonRepository, Person, DAL.DTO.Person, Guid, PersonSearch>(serviceUow, serviceRepository,
            new PersonMapper(mapper)), IPersonService;
