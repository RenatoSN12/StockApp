using FluentValidation;

namespace StockApp.Application.UseCases.Products.Update;

public sealed class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x=> x.UserId)
            .NotEmpty().WithMessage("É obrigatório estar autenticado para criar um novo produto.");
        
        RuleFor(x=> x.Dto.Description)
            .MaximumLength(500).WithMessage("A descrição do produto deve possuir no máximo 500 caracteres.");
        
        RuleFor(x=> x.Dto.CustomId)
            .NotEmpty().WithMessage("É obrigatório informar o código do produto.")
            .MaximumLength(20).WithMessage("O código do produto deve possuir no máximo 20 caracteres.");
        
        RuleFor(x=> x.Dto.Price)
            .GreaterThanOrEqualTo(0).WithMessage("O valor do produto deve ser maior ou igual a 0");
    }
}