using StockApp.Domain.Abstractions;
using StockApp.Domain.Enums;
using StockApp.Shared;

namespace StockApp.Domain.Entities;

public class Movement : Entity
{
    private Movement()
    {
    }

    private Movement(
        string userId,
        long productId,
        long? originLocationId,
        long? destinationLocationId,
        decimal quantity,
        EStockMovementType stockMovementType,
        DateTime date,
        string? description)
    {
        UserId = userId;
        ProductId = productId;
        OriginLocationId = originLocationId;
        DestinationLocationId = destinationLocationId;
        Quantity = quantity;
        MovementType = stockMovementType;
        Description = description;
    }

    public long ProductId { get; private set; }
    public Product Product { get; private set; } = null!;
    public long? OriginLocationId { get; private set; }
    public Location? OriginLocation { get; private set; }

    public long? DestinationLocationId { get; private set; }
    public Location? DestinationLocation { get; private set; }

    public EStatus Status { get; private set; }
    public decimal Quantity { get; private set; }
    public EStockMovementType MovementType { get; private set; }
    public DateTime MovementDate { get; private set; } = DateTime.Now;
    public string? Description { get; private set; }

    public static Result<Movement> Create(
        string userId, 
        long productId, 
        long? originLocationId,
        long? destinationLocationId,
        decimal quantity,
        EStockMovementType stockMovementType,
        string? description
        )
    {
        var movement = new Movement(userId, productId, originLocationId, destinationLocationId, quantity, stockMovementType, DateTime.Now,
            description);
        
        
        return Result.Success(movement);
    }
    
    private bool IsNegativeAdjustment() => MovementType == EStockMovementType.Adjustment && Quantity < 0; 
    
    // private Result<Movement> Validate(Movement movement)
    // {
    //     if (movement.MovementType == EStockMovementType.Sale || IsNegativeAdjustment())
    //     {
    //         
    //     }
    // }
}