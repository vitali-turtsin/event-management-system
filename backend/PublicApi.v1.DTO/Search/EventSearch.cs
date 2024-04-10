using Contracts.Base;

namespace PublicApi.v1.DTO.Search
{
    public class EventSearch : BaseSearch
    {
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public string? Location { get; set; }
        public override string? SortField { get; set; } = nameof(Event.Name);
    }
}
