using MediatR;
using Microsoft.Extensions.Logging;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.Mappers;
using StockApp.Application.UseCases.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Products;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Products.Update;

public sealed class UpdateProductHandler(
    IProductRepository repository,
    IUnitOfWork unitOfWork,
    UpdateProductValidator validator,
    ILogger<UpdateProductHandler> logger)
    : HandlerWithValidation<UpdateProductCommand>(validator), IRequestHandler<UpdateProductCommand, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await ValidateRequestAsync(request, cancellationToken);
            if (!result.IsSuccess)
                return Result.Failure<ProductDto>(result.Error);

            var spec = new GetProductByCustomIdSpecification(request.Dto.CustomId, request.UserId);
            var product = await repository.GetByCustomIdAsync(spec, false, cancellationToken);

            if (product is null)
                return Result<ProductDto>.Failure(new Error("404",
                    "Não foi encontrado um produto com este código customizado."));

            var updateResult = product.Update(
                request.Dto.Title,
                request.Dto.Description,
                request.Dto.Price,
                request.Dto.Status,
                request.Dto.CategoryId,
                request.Dto.ImageUrl
            );
            
            if (!updateResult.IsSuccess)
                return Result<ProductDto>.Failure(updateResult.Error);

            repository.Update(product);
            await unitOfWork.CommitAsync(cancellationToken);
            return Result<ProductDto>.Success(ProductMapper.ToDto(product));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Erro ao atualizar o produto");
            return Result<ProductDto>.Failure(new Error("500", "Ocorreu um erro inesperado ao atualizar o produto."));
        }
    }
}