using MediatR;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Categories.Delete;

public sealed record DeleteCategoryCommand(string UserId, int Id) : CommandQueryBase<Result>(UserId);