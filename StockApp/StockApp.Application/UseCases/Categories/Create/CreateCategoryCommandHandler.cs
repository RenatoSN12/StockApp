using MediatR;
using StockApp.Application.UseCases.Abstractions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;

namespace StockApp.Application.UseCases.Categories.Create;

public sealed class CreateCategoryCommandHandler(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork,
    CreateCategoryValidator validator)
    : IRequestHandler<CreateCategoryCommand, Result<CategoryDto>>, IValidatableHandler<CreateCategoryCommand>
{
    public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var category = new Category
            {
                Description = request.CreateCategoryDto.Description,
                Title = request.CreateCategoryDto.Title,
                UserId = request.UserId
            };

            var validator = new CreateCategoryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Failure<UserDto>();

            await repository.AddAsync(category, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result<CategoryDto>.Success(new CategoryDto(category.Id, category.Title, category.Description));
        }
        catch (Exception e)
        {
            return Result<CategoryDto>.Failure(new Error("500", "Ocorreu um erro inesperado ao criar a categoria."));
        }
    }

    public async Task<Result> ValidateRequestAsync(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(request, cancellationToken);
        return !result.IsValid
            ? Result.Failure(new Error("400", string.Join("\n", result.Errors.Select(e => e.ErrorMessage))))
            : Result.Success();
    }
}