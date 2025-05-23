using StockApp.Domain.Enums;

namespace StockApp.Application.DTOs.Responses.Location;

public sealed record LocationDto(long Id, string Title, string Description, EStatus Status);

// public sealed class LocationDto
// {
//     public long Id { get; set; }
//     public string Title { get; set; } = string.Empty;
//     public string Description { get; set; } = string.Empty;
//     public EStatus Status { get; set; }
// }