using MediatR;
using StockApp.Application.DTOs.Responses.Categories;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Categories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Categories.GetAll;

public sealed class GetAllCategoriesQueryHandler(ICategoryRepository repository)
    : IRequestHandler<GetAllCategoriesQuery, Result<PagedResponse<List<CategoryDto>>>>
{
    public async Task<Result<PagedResponse<List<CategoryDto>>>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var spec = new GetAllCategoriesByUserSpecification(request.UserId);
            var categories = await repository
                .GetAllByUserAsync(spec, request.PageNumber, request.PageSize, cancellationToken);

            var totalCount = await repository.GetTotalCount(spec, cancellationToken);

            var categoriesDto = categories
                .Select(x => new CategoryDto(x.Id, x.Title, x.Description))
                .ToList();

            var response =
                new PagedResponse<List<CategoryDto>?>(categoriesDto, totalCount, request.PageNumber, request.PageSize);

            return Result<PagedResponse<List<CategoryDto>>>.Success(response);
        }
        catch
        {
            return Result<PagedResponse<List<CategoryDto>>>.Failure(new Error("500",
                "Não foi possível concluir a pesquisa das categorias cadastradas."));
        }
    }
}