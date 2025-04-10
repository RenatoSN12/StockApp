using MediatR;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Requests.Categories;

namespace StockApp.Application.UseCases.Categories.Delete;

public sealed record DeleteCategoryCommand(string UserId, int Id) : CommandBase<Result>(UserId);