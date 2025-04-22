using StockApp.Domain.Enums;

namespace StockApp.Application.DTOs.Responses.Movement;

public sealed class UpdateMovementDto
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public EStatus Status { get; set; }
}