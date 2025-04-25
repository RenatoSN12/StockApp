using FluentValidation;

namespace StockApp.Application.UseCases.ProductStock.Create;

public class CreateProductStockValidator : AbstractValidator<CreateProductStockCommand>
{
    public CreateProductStockValidator()
    {
        RuleFor(x=> x.Dto.LocationId)
            .GreaterThan(0).WithMessage("É obrigatório informar o local de estoque.");
        
        RuleFor(x=> x.Dto.ProductId)
            .GreaterThan(0).WithMessage("É obrigatório informar o produto.");

        RuleFor(x => x.Dto.MinQuantity)
            .LessThan(x => x.Dto.MaxQuantity)
            .WithMessage("A quantidade mínima de estoque do produto deve ser menor que a quantidade máxima permitida.");
    }
}