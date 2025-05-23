using MediatR;

namespace StockApp.Application.UseCases.Categories;
public abstract record CommandQueryBase<TResponse>(string UserId) : IRequest<TResponse>;
