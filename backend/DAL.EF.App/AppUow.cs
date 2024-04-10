using AutoMapper;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.Contracts.App.Repositories;
using DAL.EF.App.Repositories;
using DAL.EF.Base;

namespace DAL.EF.App;

public class AppUow(AppDbContext uowDbContext, IMapper mapper) : BaseUow<AppDbContext>(uowDbContext), IAppUow
{
    private readonly IMapper Mapper = mapper;

    public IEventRepository Events =>
        GetRepository(() => new EventRepository(UowDbContext, Mapper));

    public IOrganizationRepository Organizations =>
        GetRepository(() => new OrganizationRepository(UowDbContext, Mapper));

    public IPaymentMethodRepository PaymentMethods =>
        GetRepository(() => new PaymentMethodRepository(UowDbContext, Mapper));

    public IPersonRepository People =>
        GetRepository(() => new PersonRepository(UowDbContext, Mapper));
}