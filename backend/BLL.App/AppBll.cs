using AutoMapper;
using BLL.App.Services;
using BLL.Base;
using BLL.Contracts.App;
using BLL.Contracts.App.Services;
using DAL.Contracts.App;

namespace BLL.App;

public class AppBll(IAppUow uow, IMapper mapper) : BaseBll<IAppUow>(uow), IAppBll
{
    public IEventService Events =>
        GetService(() => new EventService(Uow, Uow.Events, mapper));

    public IOrganizationService Organizations =>
        GetService(() => new OrganizationService(Uow, Uow.Organizations, mapper));

    public IPaymentMethodService PaymentMethods =>
        GetService(() => new PaymentMethodService(Uow, Uow.PaymentMethods, mapper));

    public IPersonService People =>
        GetService(() => new PersonService(Uow, Uow.People, mapper));
}