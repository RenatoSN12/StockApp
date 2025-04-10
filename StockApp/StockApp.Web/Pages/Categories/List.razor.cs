using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockApp.Domain.DTOs.Requests.Categories;
using StockApp.Domain.DTOs.Responses;
using StockApp.Domain.Entities;
using StockApp.Web.Components;
using StockApp.Web.Extensions;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Pages.Categories;

public partial class ListCategoriesPage : ComponentBase
{
    #region Fields

    protected bool IsBusy { get; set; } = false;
    protected List<CategoryDto> Categories { get; set; } = [];
    protected string SearchTerm { get; set; } = string.Empty;

    #endregion

    #region Services

    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    [Inject] protected ICategoryService CategoryService { get; set; } = null!;
    [Inject] protected IDialogService DialogService { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        try
        {
            IsBusy = true;
            var result = await CategoryService.GetAllCategoriesAsync();
            Categories = result.Value!.Data ?? [];
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

    #region Methods
    
    protected async Task OpenNewCategoryDialog()
    {
        var parameters = new DialogParameters { { "Model", new CreateCategoryDto() } };
        var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth = true };

        var dialog = await DialogService.ShowAsync<CreateCategoryDialog>("Nova Categoria", parameters, options);

        var result = await dialog.Result;

        if (!result!.Canceled && result.Data is CategoryDto novaCategoria)
        {
            Categories.Add(novaCategoria);
            Snackbar.Add("Categoria criada com sucesso!", Severity.Success);
            StateHasChanged();
        }
    }

    public async Task OnDeleteButtonClickedAsync(long id, string title)
    {
    var result = await DialogService.ShowMessageBox(
        "Atenção!",
        $"Deseja excluir a categoria {title}?",
        "Confirmar",
        cancelText: "Cancelar");
    
    if (result is true)
        await OnDeleteAsync(id, title);
    
    StateHasChanged();
    }

    private async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            var result = await CategoryService.DeleteAsync(id);
            if (result.IsSuccess)
            {
                Categories.RemoveAll(c => c.Id == id);
                Snackbar.Add($"Categoria '{title}' excluida com sucesso!", Severity.Success);
            }
            else
            {
                foreach (var error in result.Error.Message.SplitErrors())
                {
                    Snackbar.Add(error, Severity.Error);
                }
            }
        }
        catch
        {
            Snackbar.Add("Ocorreu um erro inesperado durante a exclusão da categoria.", Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    protected async Task OnEditButtonClickedAsync(CategoryDto category)
    {
        var parameters = new DialogParameters<UpdateCategoryDialog> {{"Category", category},{"Id", category.Id}};
        
        var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };
        
        var dialog = await DialogService.ShowAsync<UpdateCategoryDialog>($"{category.Title}", parameters, options);
        var result = await dialog.Result;

        if (!result!.Canceled && result.Data is CategoryDto updateCategory)
        {
            Categories.RemoveAll(c => c.Id == updateCategory.Id);
            Categories.Add(updateCategory);
            Snackbar.Add($"Categoria '{updateCategory.Title}' atualizada com sucesso!", Severity.Success);
            StateHasChanged();
        }
    }
    
    //
    // public Func<Category, bool> Filter => category =>
    // {
    //     if (string.IsNullOrEmpty(SearchTerm))
    //         return true;
    //
    //     if (category.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
    //         return true;
    //
    //     if (category.Title.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
    //         return true;
    //
    //     if (category.Description is not null &&
    //         category.Description.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
    //         return true;
    //
    //     return false;
    // };

    #endregion
}