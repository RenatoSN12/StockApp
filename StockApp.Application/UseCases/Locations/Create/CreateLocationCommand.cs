using StockApp.Application.DTOs.Requests.Locations;
using StockApp.Application.DTOs.Responses.Location;
using StockApp.Application.UseCases.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Locations.Create;

public sealed record CreateLocationCommand(string UserId, CreateLocationDto Dto) : CommandQueryBase<Result<LocationDto>>(UserId);