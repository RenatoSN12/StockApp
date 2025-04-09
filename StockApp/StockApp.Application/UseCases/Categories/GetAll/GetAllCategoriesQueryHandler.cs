using MediatR;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Categories;

namespace StockApp.Application.UseCases.Categories.GetAll;
public sealed class GetAllCategoriesQueryHandler(ICategoryRepository repository)
    : IRequestHandler<GetAllCategoriesQuery, PagedResult<List<CategoryDto>?>>
{
    public async Task<PagedResult<List<CategoryDto>?>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var spec = new GetAllCategoriesByUserSpecification(request.UserId);
            var categories = await repository
                .GetAllByUserAsync(spec, request.PageNumber, request.PageSize, cancellationToken);

            var totalCount = await repository.GetTotalCount(spec,cancellationToken);

            return PagedResult<List<CategoryDto>?>.Success(
                categories.Select(x => new CategoryDto(x.Id, x.Title)).ToList(),
                request.PageNumber, request.PageSize, totalCount
            );
        }
        catch
        {
            return PagedResult<List<CategoryDto>?>.Failure(new Error("500",
                "Não foi possível concluir a pesquisa das categorias cadastradas."));
        }
    }
}