using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Categories.Delete;

public sealed class DeleteCategoryCommandHandler(ICategoryRepository repository, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteCategoryCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var specification = new GetCategoryByIdSpecification(request.UserId, request.Id);

            var category = await repository.GetById(specification, false, cancellationToken);
            if (category == null)
                return Result.Failure(new Error("404", "Categoria não encontrada."));

            repository.Remove(category);

            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch
        {
            return Result.Failure(new Error("500", "Ocorreu um erro inesperado ao excluir a categoria."));            
        }
    }
}