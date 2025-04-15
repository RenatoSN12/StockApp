using MediatR;
using StockApp.Application.DTOs.Responses.Categories;
using StockApp.Application.UseCases.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Categories.Update;

public sealed class UpdateCategoryCommandHandler(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork,
    UpdateCategoryValidator validator)
    : HandlerWithValidation<UpdateCategoryCommandQuery>(validator), IRequestHandler<UpdateCategoryCommandQuery, Result<CategoryDto>>
{
    public async Task<Result<CategoryDto>> Handle(UpdateCategoryCommandQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new GetCategoryByIdSpecification(request.UserId, request.Id);
            var category = await repository.GetById(spec, false, cancellationToken);

            if (category == null)
                return Result<CategoryDto>.Failure(new Error("404", "Categoria não encontrada."));

            var validationResult = await ValidateRequestAsync(request, cancellationToken);
            if (validationResult.IsFailure)
                return Result.Failure<CategoryDto>(validationResult.Error);
            
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