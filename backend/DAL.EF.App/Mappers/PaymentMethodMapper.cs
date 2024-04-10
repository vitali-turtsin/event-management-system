using Contracts.Base;
using DAL.DTO;

namespace DAL.EF.App.Mappers;

public class PaymentMethodMapper(AutoMapper.IMapper mapper) : BaseMapper<PaymentMethod, Domain.App.PaymentMethod>(mapper);