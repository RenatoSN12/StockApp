using MediatR;
using Microsoft.Extensions.Logging;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.Mappers;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Products;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Products.Activate;

public sealed class ActivateProductHandler(ILogger<ActivateProductHandler> logger, IProductRepository repository, IUnitOfWork unitOfWork) 
    : IRequestHandler<ActivateProductCommand, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new GetProductByIdSpecification(request.ProductId, request.UserId);
            var product = await repository.GetProduct(spec, false, cancellationToken);

            if (product == null)
                return Result<ProductDto>.Failure(new Error("404", "Não foi encontrado um produto com o código informado."));

            var resultInactivate = product.Activate();
            if (resultInactivate.IsFailure)
                return Result<ProductDto>.Failure(resultInactivate.Error);

            await unitOfWork.CommitAsync(cancellationToken);
            return Result<ProductDto>.Success(ProductMapper.ToDto(product));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occurred while trying to activate product with ID {ProductId}", request.ProductId);
            return Result.Failure<ProductDto>(new Error("500",
                "Ocorreu um erro inesperado durante a reativação do produto."));
        }
    }
}