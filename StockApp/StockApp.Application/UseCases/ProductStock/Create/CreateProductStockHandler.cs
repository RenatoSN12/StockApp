using MediatR;
using Microsoft.Extensions.Logging;
using StockApp.Application.DTOs.Responses.ProductStock;
using StockApp.Application.Mappers;
using StockApp.Application.UseCases.Abstractions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Locations;
using StockApp.Domain.Specification.Products;
using StockApp.Domain.Specification.ProductStock;
using StockApp.Shared;

namespace StockApp.Application.UseCases.ProductStock.Create;

public sealed class CreateProductStockHandler(IProductStockRepository repository,
    IUnitOfWork unitOfWork,
    ILocationRepository locationRepository,
    IProductRepository productRepository,
    ILogger<CreateProductStockHandler> logger,
    CreateProductStockValidator validator) 
    : HandlerWithValidation<CreateProductStockCommand>(validator), IRequestHandler<CreateProductStockCommand, Result<ProductStockDto>>
{
    public async Task<Result<ProductStockDto>> Handle(CreateProductStockCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await ValidateRequestAsync(request, cancellationToken);
            if(validationResult.IsFailure)
                return Result<ProductStockDto>.Failure(validationResult.Error);
            
            var newProductStockResult = Domain.Entities.ProductStock.Create(
                request.UserId,
                request.Dto.ProductId, 
                request.Dto.LocationId,
                request.Dto.MinQuantity,
                request.Dto.MaxQuantity
                );
            
            if (newProductStockResult.IsFailure)
                return Result<ProductStockDto>.Failure(newProductStockResult.Error);
                
            await repository.Insert(newProductStockResult.Value!, cancellationToken);          
            await unitOfWork.CommitAsync(cancellationToken);

            
            // Reload para recarregar a tabela Location e Product.
            var productStockReloaded = await ReloadProductStock(new GetProductStockSpecification(
                request.UserId,
                request.Dto.ProductId,
                request.Dto.LocationId
            ));
            
            return Result<ProductStockDto>.Success(productStockReloaded!.ToDto());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Erro ao tentar criar item stock");
            return Result<ProductStockDto>.Failure(new Error("500",
                "Ocorreu um erro inesperado ao criar o relacionamento item / local de estoque."));
        }
    }
    
    private async Task<Domain.Entities.ProductStock?> ReloadProductStock(GetProductStockSpecification specification)
        => await repository.GetProductStock(specification, true);
    public override async Task<Result> ValidateRequestAsync(CreateProductStockCommand request, CancellationToken cancellationToken = default)
    {
        var specProduct = new GetProductByIdSpecification(request.Dto.ProductId, request.UserId);
        var product = await productRepository.GetProduct(specProduct, true, cancellationToken);

        if (product == null)
            return Result.Failure(new Error("400", "Não foi encontrado um produto com o ID informado."));
            
        var specLocation = new GetLocationByIdSpecification(request.Dto.LocationId, request.UserId);
        var location = await locationRepository.GetLocationAsync(specLocation, true, cancellationToken);
        
        if (location == null)
            return Result.Failure(new Error("400", "Não foi encontrado um local de estoque com o ID informado."));
        
        return await base.ValidateRequestAsync(request, cancellationToken);
    }
}