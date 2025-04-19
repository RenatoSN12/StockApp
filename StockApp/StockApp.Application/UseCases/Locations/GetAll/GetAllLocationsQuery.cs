using StockApp.Application.DTOs.Responses.Location;
using StockApp.Application.UseCases.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Locations.GetAll;

public sealed record GetAllLocationsQuery(string UserId, int PageNumber, int PageSize) 
    : CommandQueryBase<Result<PagedResponse<IEnumerable<LocationDto>>>>(UserId);