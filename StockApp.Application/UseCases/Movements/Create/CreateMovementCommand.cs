using StockApp.Application.DTOs.Requests.Movements;
using StockApp.Application.DTOs.Responses.Movement;
using StockApp.Application.UseCases.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Movements.Create;

public sealed record CreateMovementCommand(string UserId, CreateMovementDto Dto) : CommandQueryBase<Result<MovementDto>>(UserId);