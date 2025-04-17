using Microsoft.AspNetCore.Components;
using StockApp.Application.DTOs.Responses.Products;
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
    
    #endregion
    
    #region Overrides

    protected override async Task OnParametersSetAsync()
    {
        var result = await ProductService.GetByCustomId(CustomId);
        if (result.IsSuccess && result.Value is not null)
        {
            Product.CustomId = result.Value.CustomId;
            Product.Title = result.Value.Title;
            Product.Description = result.Value.Description;
            Product.Price = result.Value.Price;

            StateHasChanged(); // força render se necessário
        }
    }

    #endregion
    
    #region Methods

    // private async Task GetProduct()
    // {
    //     var result = await ProductService.GetByCustomId(CustomId);
    //     if (result.IsSuccess && result.Value is not null)
    //     {
    //         Product.CustomId = result.Value.CustomId;
    //         Product.Title = result.Value.Title;
    //         Product.Description = result.Value.Description;
    //         Product.Price = result.Value.Price;
    //     }
    // }

    #endregion

}