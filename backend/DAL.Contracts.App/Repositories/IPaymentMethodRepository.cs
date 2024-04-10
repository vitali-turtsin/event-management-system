using DAL.Contracts.Base.Repositories;
using DAL.DTO.Search;
using DAL.DTO;

namespace DAL.Contracts.App.Repositories;

public interface IPaymentMethodRepository : IBaseRepository<PaymentMethod, Guid, PaymentMethodSearch>,
    IPaymentMethodRepositoryCustom<PaymentMethod>;

public interface IPaymentMethodRepositoryCustom<TEntity>;