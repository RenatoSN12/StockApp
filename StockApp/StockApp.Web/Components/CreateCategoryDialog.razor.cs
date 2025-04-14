using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockApp.Application.DTOs.Requests.Categories;
using StockApp.Web.Extensions;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Components;

public partial class CreateCategoryDialogBase : ComponentBase
{
    #region Parameters

    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;
    [Parameter] public CreateCategoryDto Model { get; set; } = new();

    #endregion

    #region Properties

    protected bool IsBusy { get; set; }
    
    #endregion
    
    #region Services

    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] public ICategoryService CategoryService { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    
    #endregion

    protected async Task<DialogResult> OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await CategoryService.CreateCategoryAsync(Model);
            if (result.IsSuccess)
                MudDialog.Close(DialogResult.Ok(result.Value));
            else
            {
                foreach (var error in result.Error.Message.SplitErrors())
                {
                    Snackbar.Add(error, Severity.Error);
                }
                return DialogResult.Cancel();
            }
        }
        catch (Exception e)
        {
            Snackbar.Add("Ocorreu um erro inesperado ao salvar a categoria.", Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
        return DialogResult.Cancel();
    }

    protected void Cancel()
    {
        MudDialog.Close(DialogResult.Cancel());
    }
}