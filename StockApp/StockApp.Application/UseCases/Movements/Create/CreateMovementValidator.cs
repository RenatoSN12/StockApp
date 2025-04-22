using FluentValidation;
using StockApp.Application.DTOs.Responses.Movement;

namespace StockApp.Application.UseCases.Movements.Create;

public class CreateMovementValidator : AbstractValidator<CreateMovementDto>
{
    public CreateMovementValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("O produto é obrigatório.");
        
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

        RuleFor(x => x.MovementType)
            .IsInEnum().WithMessage("O tipo de movimentação é inválido.");
        
        RuleFor(x=> x.Description)
            .MaximumLength(500).WithMessage("A descrição não pode possuir mais que 500 caracteres.");
    }
}