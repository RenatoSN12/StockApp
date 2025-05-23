namespace StockApp.Application.DTOs.Requests.Locations;

public sealed class CreateLocationDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}