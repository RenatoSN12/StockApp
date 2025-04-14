using MediatR;
using StockApp.Application.DTOs.Responses.Categories;
using StockApp.Application.UseCases.Abstractions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.DTOs.Responses;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Categories.Create;

public sealed class CreateCategoryCommandHandler(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork,
    CreateCategoryValidator validator) 
    : HandlerWithValidation<CreateCategoryCommand>(validator), IRequestHandler<CreateCategoryCommand, Result<CategoryDto>>
{
    public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var category = new Category
            {
                Description = request.Dto.Description,
                Title = request.Dto.Title,
                UserId = request.UserId
            };

            var validationResult = await ValidateRequestAsync(request, cancellationToken);
            if (validationResult.IsFailure)
                return Result.Failure<CategoryDto>(validationResult.Error);

            await repository.AddAsync(category, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result<CategoryDto>.Success(new CategoryDto(category.Id, category.Title, category.Description));
        }
        catch (Exception e)
        {
            return Result<CategoryDto>.Failure(new Error("500", "Ocorreu um erro inesperado ao criar a categoria."));
        }
    }
}