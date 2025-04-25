using MediatR;
using Microsoft.Extensions.Logging;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.ProductStock;
using StockApp.Shared;

namespace StockApp.Application.UseCases.ProductStock.Delete;

public class DeleteProductStockHandler(IProductStockRepository repository, IUnitOfWork unitOfWork, ILogger<DeleteProductStockHandler> logger) 
    : IRequestHandler<DeleteProductStockCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteProductStockCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new GetProductStockSpecification(request.UserId, request.Dto.ProductId, request.Dto.LocationId);
            var productStock = await repository.GetProductStock(spec, true, cancellationToken);

            if (productStock == null)
                return Result<string>.Failure(new Error("404",
                    "Não foi encontrado relacionamento entre item e local de estoque para os parâmetros informados."));

            repository.Delete(productStock);
            await unitOfWork.CommitAsync(cancellationToken);
            
            return Result<string>.Success($"Deletado o vínculo entre o item {productStock.Product.Title} e local de estoque {productStock.Location.Title}");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Erro ao tentar excluir item stock");
            return Result<string>.Failure(new Error("500",
                "Ocorreu um erro inesperado ao apagar o relacionamento item / local de estoque."));
        }
    }
}