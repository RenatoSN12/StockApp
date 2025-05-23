using System.Data;
using FluentValidation;

namespace StockApp.Application.UseCases.Locations.Create;

public sealed class CreateLocationValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationValidator()
    {
        RuleFor(x=> x.UserId)
            .NotEmpty().WithMessage("É obrigatório estar autenticado para criar um novo local.");

        RuleFor(x => x.Dto.Title)
            .NotEmpty().WithMessage("É obrigatório informar o título do local.")
            .MaximumLength(80).WithMessage("O título do local deve possuir no máximo 80 caracteres.");
        
        RuleFor(x => x.Dto.Description)
            .MaximumLength(255).WithMessage("A descrição do local deve possuir no máximo 255 caracteres.");
    }
}