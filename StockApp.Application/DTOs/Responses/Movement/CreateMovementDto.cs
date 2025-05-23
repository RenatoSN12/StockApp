using StockApp.Domain.Enums;

namespace StockApp.Application.DTOs.Responses.Movement;

public sealed class CreateMovementDto
{
    public long ProductId { get; set; }
    public long? OriginLocationId { get; set; }
    public long? DestinationLocationId { get; set; }
    public decimal Quantity { get; set; }
    public EStockMovementType MovementType { get; set; }
    public string? Description { get; set; }
}