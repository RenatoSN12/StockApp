using MediatR;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.Mappers;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Products;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Products.GetByCustomId;

public sealed class GetProductByCustomIdHandler(IProductRepository repository) 
    : IRequestHandler<GetProductByCustomIdQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(GetProductByCustomIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new GetProductByCustomIdSpecification(request.ProductId,request.UserId);
            var product = await repository.GetProduct(spec,true ,cancellationToken);

            return product is null
                ? Result.Failure<ProductDto>(new Error("404", "Não foi encontrado um produto com o código informado."))
                : Result.Success(ProductMapper.ToDto(product));
        }
        catch (Exception e)
        {
            return Result.Failure<ProductDto>(new Error("500", "Ocorreu um erro inesperado ao carregar a categoria."));
        }
    }
    
}