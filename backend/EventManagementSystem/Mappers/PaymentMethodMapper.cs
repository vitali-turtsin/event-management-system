using Contracts.Base;
using PublicApi.v1.DTO;

namespace EventManagementSystem.Mappers;

public class PaymentMethodMapper(AutoMapper.IMapper mapper) : BaseMapper<PaymentMethod, BLL.DTO.PaymentMethod>(mapper);