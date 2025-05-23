using MediatR;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.Mappers;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Products;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Products.GetAll;

public sealed class GetAllProductsHandler(IProductRepository repository)
    : IRequestHandler<GetAllProductsQuery, Result<PagedResponse<List<ResumeProductDto>>>>
{
    public async Task<Result<PagedResponse<List<ResumeProductDto>>>> Handle(GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var spec = new GetAllProductsByUserSpecification(request.UserId);

            var products = await repository
                .GetAllAsync(spec, request.PageNumber, request.PageSize, cancellationToken);

            var productsDto = ProductMapper.ToResumedDtoList(products ?? []);

            var totalCount = await repository.GetTotalCount(spec, cancellationToken);

            return Result<PagedResponse<List<ResumeProductDto>>>.Success(
                new PagedResponse<List<ResumeProductDto>>(productsDto, totalCount, request.PageNumber, request.PageSize));
        }
        catch (Exception e)
        {
            return Result.Failure<PagedResponse<List<ResumeProductDto>>>(new Error("500",
                "Ocorreu um erro inesperado ao carregar os produtos."));
        }
    }
}