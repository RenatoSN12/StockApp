using System.Text.Json.Serialization;

namespace StockApp.Domain.DTOs.Responses;

public class PagedResponse<TData>
{
    [JsonConstructor]
    public PagedResponse(
        TData? data,
        int totalCount,
        int currentPage = 1,
        int pageSize = 25
    )
    {
        Data = data;
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }

    public PagedResponse(TData? data){ Data = data; }
    public TData? Data { get; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public int TotalCount { get; set; }
}