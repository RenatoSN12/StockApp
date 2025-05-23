using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Web.Extensions;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Pages.Products;

public partial class ListProductsPage : ComponentBase
{
    #region Fields
    protected List<ResumeProductDto> Products { get; set; } = [];
    protected string SearchTerm { get; set; } = string.Empty;

    #endregion
    
    #region Services

    [Inject] public IProductService ProductService { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    
    #endregion
    
    #region Methods

    protected override async Task OnInitializedAsync()
    {
        var result = await ProductService.GetAllAsync();

        if (result.IsFailure)
        {
            foreach (var error in result.Error.Message.SplitErrors())
            {
                Snackbar.Add(error, Severity.Error);
            }
        }
        Products = result.Value!.Data ?? [];
    }
    
    public Func<ResumeProductDto, bool> Filter => product =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;
    
        if (product.CustomId.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
    
        if (product.Title.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    };

    #endregion
}