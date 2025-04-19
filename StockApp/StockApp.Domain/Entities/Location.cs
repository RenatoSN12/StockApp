using StockApp.Domain.Abstractions;
using StockApp.Domain.Enums;
using StockApp.Shared;

namespace StockApp.Domain.Entities;

public class Location : Entity
{
    private Location() { }

    private Location(string userId, string title, string description, EStatus status)
    {
        UserId = userId;
        Title = title;
        Description = description;
        Status = status;
    }

    public static Result<Location> Create(string userId, string title, string description, EStatus status)
    {
        var location = new Location(userId, title, description, status);
        return Result.Success(location);
        
        // Por enquanto não há validações de negócio
    }
    
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public EStatus Status { get; private set; } = EStatus.Active;
}