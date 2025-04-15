using Microsoft.AspNetCore.Components;
using StockApp.Application.DTOs.Responses.Categories;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Pages.Products;

public class CreateProductPage : ComponentBase
{
    #region Fields
    protected ProductDto Product { get; set; } = null!;
    protected List<CategoryDto> Categories { get; set; } = [];

    #endregion
    
    #region Services

    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public ICategoryService CategoryService { get; set; } = null!;
    
    #endregion
    
    #region Methods
    #endregion
    
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        Product = new ProductDto();
        var result = await CategoryService.GetAllCategoriesAsync();
        Categories = result.Value?.Data ?? [];
        Product.CategoryId = Categories.FirstOrDefault()?.Id ?? 0;
    }

    #endregion
}