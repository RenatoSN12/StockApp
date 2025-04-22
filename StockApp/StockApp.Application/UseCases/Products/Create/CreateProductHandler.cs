using MediatR;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.Mappers;
using StockApp.Application.UseCases.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Repositories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Products.Create;

public class CreateProductHandler(IProductRepository repository, IUnitOfWork unitOfWork, CreateProductValidator validator)
    : HandlerWithValidation<CreateProductCommand>(validator), IRequestHandler<CreateProductCommand, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await ValidateRequestAsync(request, cancellationToken);

            if(!result.IsSuccess)
                return Result.Failure<ProductDto>(result.Error);
            
            var newProductResult = Product.Create(request.UserId, request.Dto.CustomId, request.Dto.Title,
                request.Dto.Description, request.Dto.Price, request.Dto.Status, request.Dto.CategoryId,
                request.Dto.ImageUrl);

            if (newProductResult.IsFailure)
                return Result<ProductDto>.Failure(newProductResult.Error); 
            
            var product = newProductResult.Value!;
            
            await repository.AddAsync(product, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            
            return Result<ProductDto>.Success(ProductMapper.ToDto(product));
        }
        catch(Exception ex)
        {
            return Result<ProductDto>.Failure(new Error("500", "Ocorreu um erro inesperado ao criar o produto."));
        }
    }
}