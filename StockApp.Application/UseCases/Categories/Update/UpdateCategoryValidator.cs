using FluentValidation;

namespace StockApp.Application.UseCases.Categories.Update;

public sealed class UpdateCategoryValidator : AbstractValidator<UpdateCategoryQuery>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Dto.Title)
            .NotEmpty().WithMessage("É obrigatório informar o título da categoria.")
            .MaximumLength(80).WithMessage("O título da categoria deve haver no máximo 80 caracteres.");
        
        RuleFor(x => x.Dto.Description)
            .NotEmpty().WithMessage("É obrigatório informar a descrição da categoria.")
            .MaximumLength(80).WithMessage("A descrição da categoria deve haver no máximo 500 caracteres.");
    }
}