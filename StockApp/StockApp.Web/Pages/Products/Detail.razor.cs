using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Web.Extensions;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Pages.Products;

public partial class DetailProductPage : ComponentBase
{
    #region Fields

    [EditorRequired]
    public ProductDto Product { get; set; } = new();
    
    [Parameter]
    public string CustomId { get; set; } = string.Empty;

    #endregion

    #region Services
    
    [Inject] private IProductService ProductService { get; set; } = null!;
    [Inject] private ICategoryService CategoryService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
    
    #endregion
    
    #region Overrides

    protected override async Task OnParametersSetAsync()
    {
        var result = await ProductService.GetByCustomId(CustomId);
        if (result is { IsSuccess: true, Value: not null })
        {
            Product = result.Value;
            StateHasChanged();
        }
    }

    #endregion
    
    #region Methods

    protected void Cancel() => NavigationManager.NavigateTo("/products");

    protected async Task SaveChanges()
    {
        var result = await ProductService.UpdateAsync(Product);
        if (result.IsFailure)
        {
            foreach (var erro in result.Error.Message.SplitErrors())
            {
                Snackbar.Add(erro, Severity.Error);
            }            
        }
        else
        {
            Snackbar.Add("Produto atualizado com sucesso!", Severity.Success);
            NavigationManager.NavigateTo("/products");
        }
            
    }
    
    #endregion

}