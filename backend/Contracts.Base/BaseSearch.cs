using Domain.Contracts.Base;

namespace Contracts.Base;

public class BaseSearch : ISearch
{
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public bool IsPagingEnabled { get; set; } = true;
    public ICollection<Guid>? Ids { get; set; }
    public virtual string? SortField { get; set; } = nameof(IDomainEntityId.Id);
    public bool IsAscending { get; set; } = true;
}