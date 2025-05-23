using StockApp.Application.DTOs.Responses.Location;
using StockApp.Domain.Entities;

namespace StockApp.Application.Mappers;

public static class LocationMapper
{
    public static LocationDto ToDto(this Location location) =>
        new LocationDto(location.Id, location.Title, location.Description, location.Status);
}