using BLL.Contracts.Base.Services;
using BLL.DTO;
using DAL.DTO.Search;

namespace BLL.Contracts.App.Services;

public interface IPaymentMethodService : IBaseService<PaymentMethod, DAL.DTO.PaymentMethod, Guid, PaymentMethodSearch>;