using System.Text.Json.Serialization;

namespace StockApp.Shared;

[method: JsonConstructor]
public class PagedResponse<TData>(
    TData? data,
    int totalCount,
    int currentPage = 1,
    int pageSize = 25)
{
    public PagedResponse(TData? data) : this(data, 0, 0, 0)
    {
    }
    public TData? Data { get; } = data;
    public int CurrentPage { get; set; } = currentPage;
    public int PageSize { get; set; } = pageSize;
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public int TotalCount { get; set; } = totalCount;
}