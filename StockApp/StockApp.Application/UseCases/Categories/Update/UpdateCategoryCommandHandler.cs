using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Categories;

namespace StockApp.Application.UseCases.Categories.Update;

public sealed class UpdateCategoryCommandHandler(ICategoryRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCategoryCommand, Result<CategoryDto>>
{
    public async Task<Result<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new GetCategoryByIdSpecification(request.UserId, request.Id);
            var category = await repository.GetById(spec, false, cancellationToken);

            if (category == null)
                return Result<CategoryDto>.Failure(new Error("404", "Categoria não encontrada."));

            category.Title = request.Dto.Title;
            category.Description = request.Dto.Description;

            repository.Update(category);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result<CategoryDto>.Success(new CategoryDto(category.Id, category.Title, category.Description));
        }
        catch
        {
            return Result<CategoryDto>.Failure(new Error("500",
                "Ocorreu um erro inesperado durante a atualização da categoria."));
        }
    }
}