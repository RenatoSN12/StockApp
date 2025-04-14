using FluentValidation;

namespace StockApp.Application.UseCases.Categories.Create;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.CreateCategoryDto)
            .NotNull().WithMessage("Preencha todos os campos obrigatórios para a gravação de uma nova categoria.");
        
        RuleFor(x => x.CreateCategoryDto.Title)
            .NotEmpty().WithMessage("É obrigatório informar o título da categoria.")
            .MaximumLength(80).WithMessage("O título da categoria deve haver no máximo 80 caracteres.");
        
        RuleFor(x => x.CreateCategoryDto.Description)
            .NotEmpty().WithMessage("É obrigatório informar a descrição da categoria.")
            .MaximumLength(80).WithMessage("A descrição da categoria deve haver no máximo 500 caracteres.");
    }
}