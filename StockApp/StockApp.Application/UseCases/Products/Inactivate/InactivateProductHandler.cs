using MediatR;
using Microsoft.Extensions.Logging;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.Mappers;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Products;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Products.Inactivate;

public sealed class InactivateProductHandler(ILogger<InactivateProductHandler> logger, IProductRepository repository, IUnitOfWork unitOfWork) 
    : IRequestHandler<InactivateProductCommand,Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(InactivateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new GetProductByIdSpecification(request.ProductId, request.UserId);
            var product = await repository.GetProduct(spec, false, cancellationToken);

            if (product == null)
                return Result<ProductDto>.Failure(new Error("404", "Não foi encontrado um produto com o código informado."));

            var resultInactivate = product.Inactive();
            if (resultInactivate.IsFailure)
                return Result<ProductDto>.Failure(resultInactivate.Error);

            await unitOfWork.CommitAsync(cancellationToken);
            return Result<ProductDto>.Success(ProductMapper.ToDto(product));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occurred while trying to inactivate product with ID {ProductId}", request.ProductId);
            return Result.Failure<ProductDto>(new Error("500",
                "Ocorreu um erro inesperado durante a inativação do produto."));
        }
    }
}