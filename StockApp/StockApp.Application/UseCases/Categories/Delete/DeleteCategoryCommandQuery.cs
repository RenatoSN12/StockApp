using MediatR;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Categories.Delete;

public sealed record DeleteCategoryCommandQuery(string UserId, int Id) : CommandQueryBase<Result>(UserId);