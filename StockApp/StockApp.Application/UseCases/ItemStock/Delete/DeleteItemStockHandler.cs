using MediatR;
using Microsoft.Extensions.Logging;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.ItemStock;
using StockApp.Shared;

namespace StockApp.Application.UseCases.ItemStock.Delete;

public class DeleteItemStockHandler(IItemStockRepository repository, IUnitOfWork unitOfWork, ILogger<DeleteItemStockHandler> logger) 
    : IRequestHandler<DeleteItemStockCommand, Result>
{
    public async Task<Result> Handle(DeleteItemStockCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new GetItemStockSpecification(request.Dto.ProductId, request.Dto.LocationId);
            var itemStock = await repository.GetItemStock(spec, cancellationToken);

            if (itemStock == null)
                return Result.Failure(new Error("404",
                    "Não foi encontrado relacionamento entre item e local de estoque para os parâmetros informados."));

            repository.Delete(itemStock);
            await unitOfWork.CommitAsync(cancellationToken);
            
            return Result.Success();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Erro ao tentar excluir item stock");
            return Result.Failure(new Error("500",
                "Ocorreu um erro inesperado ao apagar o relacionamento item / local de estoque."));
        }
    }
}