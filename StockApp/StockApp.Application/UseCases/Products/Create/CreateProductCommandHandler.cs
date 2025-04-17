using MediatR;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.Mappers;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories.Products;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Products.Create;

public class CreateProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProductCommand, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newProductResult = Product.Create(request.UserId, request.Dto.CustomId, request.Dto.Title,
                request.Dto.Description, request.Dto.Price, request.Dto.Status, request.Dto.CategoryId,
                request.Dto.ImageUrl);

            if (newProductResult.IsFailure)
                return Result<ProductDto>.Failure(newProductResult.Error); 
            
            var product = newProductResult.Value!;
            
            await repository.CreateAsync(product, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            
            return Result<ProductDto>.Success(ProductMapper.ToDto(product));
        }
        catch(Exception ex)
        {
            return Result<ProductDto>.Failure(new Error("500", "Ocorreu um erro inesperado ao criar o produto."));
        }
    }
}