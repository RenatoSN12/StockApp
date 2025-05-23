using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockApp.Application.DTOs.Requests.Products;
using StockApp.Application.DTOs.Responses.Categories;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Web.Extensions;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Pages.Products;

public class CreateProductPage : ComponentBase
{
    #region Fields
    protected CreateProductDto Product { get; set; } = null!;
    protected List<CategoryDto> Categories { get; set; } = [];

    #endregion
    
    #region Services

    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public ICategoryService CategoryService { get; set; } = null!;
    [Inject] public IProductService ProductService { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    
    #endregion
    
    #region Methods

    protected async Task CreateProduct()
    {
        try
        {
            var result = await ProductService.CreateAsync(Product);
            if (result.IsFailure)
            {
                foreach (var error in result.Error.Message.SplitErrors())
                {
                    Snackbar.Add(error, Severity.Error);
                }
            }
            else
            {
                Snackbar.Add("Item criado com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/products");
            }
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
    }
    
    protected void Cancel() => NavigationManager.NavigateTo("/products");
    
    #endregion
    
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        Product = new CreateProductDto() {CategoryId = 0};
        var result = await CategoryService.GetAllCategoriesAsync();
        Categories = result.Value?.Data ?? [];
    }

    #endregion
}