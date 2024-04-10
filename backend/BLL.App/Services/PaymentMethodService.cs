using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using DAL.DTO.Search;
using BLL.DTO;
using BLL.Contracts.App.Services;
using DAL.Contracts.App.Repositories;
using DAL.Contracts.App;

namespace BLL.App.Services;

public class PaymentMethodService(IAppUow serviceUow, IPaymentMethodRepository serviceRepository, IMapper mapper)
    :
        BaseService<IAppUow, IPaymentMethodRepository, PaymentMethod, DAL.DTO.PaymentMethod, Guid, PaymentMethodSearch>(serviceUow, serviceRepository,
            new PaymentMethodMapper(mapper)), IPaymentMethodService;
