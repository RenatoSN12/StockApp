using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockApp.Application.DTOs.Requests.Categories;
using StockApp.Application.DTOs.Responses.Categories;
using StockApp.Web.Extensions;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Components;

public partial class UpdateCategoryDialogBase : ComponentBase
{
    #region Services

    [Inject] private ICategoryService CategoryService { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    
    #endregion
    
    #region Fields
    
    [Parameter]
    [EditorRequired]
    public CategoryDto Category { get; set; } = null!;
    public UpdateCategoryDto Model { get; set; } = null!;
    
    [Parameter]
    [EditorRequired]
    public long Id { get; set; }
    
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = null!;
    
    protected bool IsBusy { get; set; }

    #endregion
    
    #region Overrides

    protected override Task OnInitializedAsync()
    {
        Model = new UpdateCategoryDto
        {
            Description = Category.Description,
            Title = Category.Title
        };
        return base.OnInitializedAsync();
    }

    #endregion
    
    #region Methods

    protected async Task<DialogResult> OnValidSubmitAsync()
    {
        IsBusy = true;
        
        try
        {
            var result = await CategoryService.UpdateAsync(Id, Model);
            if (result.IsSuccess)
            {
                MudDialog.Close(DialogResult.Ok(result.Value));
            }
            else
            {
                foreach (var error in result.Error.Message.SplitErrors())
                {
                    Snackbar.Add(error, Severity.Error);
                }
                MudDialog.Close(DialogResult.Cancel());
            }
        }
        catch (Exception e)
        {
            Snackbar.Add("Ocorreu um erro inesperado ao atualizar a categoria.", Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
        return DialogResult.Cancel();
    }

    protected void Cancel()
    {
        MudDialog.Cancel();
    }
    
    #endregion

}