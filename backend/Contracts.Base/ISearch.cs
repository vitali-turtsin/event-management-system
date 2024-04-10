namespace Contracts.Base;

public interface ISearch
{
    int PageNumber { get; set; }
    int PageSize { get; set; }
    bool IsPagingEnabled { get; set; }
    string? SortField { get; set; }
    bool IsAscending { get; set; }
}