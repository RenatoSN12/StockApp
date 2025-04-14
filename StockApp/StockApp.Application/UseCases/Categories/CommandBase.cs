using MediatR;

namespace StockApp.Application.UseCases.Categories;
public abstract record CommandBase<TResponse>(string UserId) : IRequest<TResponse>;
