using BLL.DTO;
using Contracts.Base;

namespace BLL.App.Mappers;

public class PaymentMethodMapper(AutoMapper.IMapper mapper) : BaseMapper<PaymentMethod, DAL.DTO.PaymentMethod>(mapper);