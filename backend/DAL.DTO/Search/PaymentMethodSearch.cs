using Contracts.Base;

namespace DAL.DTO.Search
{
    public class PaymentMethodSearch : BaseSearch
    {
        public string? Name { get; set; }
        public override string? SortField { get; set; } = nameof(PaymentMethod.Name);
    }
}
