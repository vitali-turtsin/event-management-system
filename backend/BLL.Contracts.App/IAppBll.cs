using BLL.Contracts.App.Services;
using BLL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IAppBll : IBaseBll
{
    IEventService Events { get; }
    IOrganizationService Organizations { get; }
    IPersonService People { get; }
    IPaymentMethodService PaymentMethods { get; }
}