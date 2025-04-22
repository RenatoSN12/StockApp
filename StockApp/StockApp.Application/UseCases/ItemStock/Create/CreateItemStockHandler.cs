using MediatR;
using Microsoft.Extensions.Logging;
using StockApp.Application.DTOs.Responses.ItemStock;
using StockApp.Application.Mappers;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Repositories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.ItemStock.Create;

public sealed class CreateItemStockHandler(IItemStockRepository repository, IUnitOfWork unitOfWork, ILogger<CreateItemStockHandler> logger) 
    : IRequestHandler<CreateItemStockCommand, Result<ItemStockDto>>
{
    public async Task<Result<ItemStockDto>> Handle(CreateItemStockCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newItemStockResult = Domain.Entities.ItemStock.Create(
                request.Dto.ProductId, 
                request.Dto.LocationId,
                request.Dto.MinQuantity,
                request.Dto.MaxQuantity
                );
            
            if (newItemStockResult.IsFailure)
                return Result<ItemStockDto>.Failure(newItemStockResult.Error);
                
            await repository.Insert(newItemStockResult.Value!, cancellationToken);          
            await unitOfWork.CommitAsync(cancellationToken);
            
            return Result<ItemStockDto>.Success(newItemStockResult.Value!.ToDto());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Erro ao tentar excluir item stock");
            return Result<ItemStockDto>.Failure(new Error("500",
                "Ocorreu um erro inesperado ao apagar o relacionamento item / local de estoque."));
        }
    }
}