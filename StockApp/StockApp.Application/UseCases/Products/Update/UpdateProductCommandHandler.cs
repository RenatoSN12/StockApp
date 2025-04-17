using MediatR;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.Mappers;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Repositories.Products;
using StockApp.Domain.Specification.Products;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Products.Update;

public sealed class UpdateProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateProductCommand, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new GetProductByCustomIdSpecification(request.Dto.CustomId, request.UserId);
            var product = await repository.GetByCustomIdAsync(spec, false, cancellationToken);

            if (product is null)
                return Result<ProductDto>.Failure(new Error("404",
                    "Não foi encontrado um produto com este código customizado."));

            product.CategoryId = request.Dto.CategoryId;
            product.Description = request.Dto.Description;
            product.Price = request.Dto.Price;
            product.ImageUrl = request.Dto.ImageUrl;
            product.UpdatedAt = DateTime.Now;
            product.Status = request.Dto.Status;
            product.Title = request.Dto.Title;

            repository.Update(product);
            await unitOfWork.CommitAsync(cancellationToken);
            return Result<ProductDto>.Success(ProductMapper.ToDto(product));
        }
        catch (Exception e)
        {
            return Result<ProductDto>.Failure(new Error("500", "Ocorreu um erro inesperado ao atualizar o produto."));
        }
    }
}