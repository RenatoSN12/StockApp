using StockApp.Domain.Enums;

namespace StockApp.Application.DTOs.Requests.Movements;

public sealed record MovementDto(
    long Id,
    string ProductCustomId,
    string? OriginLocationName,
    string? DestinationLocationName,
    decimal Quantity,
    EStockMovementType MovementType,
    DateTime MovementDate,
    string? Description
);