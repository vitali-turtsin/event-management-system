using DAL.Contracts.App.Repositories;
using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IAppUow : IBaseUow
{
    IEventRepository Events { get; }
    IOrganizationRepository Organizations { get; }
    IPersonRepository People { get; }
    IPaymentMethodRepository PaymentMethods { get; }
}