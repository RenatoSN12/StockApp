using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using StockApp.Application.DTOs.Responses.Categories;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Components;

public partial class CategorySelectorPage : ComponentBase
{
    #region Fields

    public List<CategoryDto> Categories { get; set; } = [];
    [Parameter] public long Value { get; set; }

    [Parameter] public EventCallback<long> ValueChanged { get; set; }

    [Parameter] public Expression<Func<long>>? ValueExpression { get; set; }
    #endregion
    
    #region Services

    [Inject] private ICategoryService CategoryService { get; set; } = null!;
    
    #endregion
    
    #region Overrides

    protected override async Task OnParametersSetAsync()
    {
        var categories = await CategoryService.GetAllCategoriesAsync();
        Categories = categories.Value?.Data ?? [];
    }

    #endregion
    
    #region Methods
    protected async Task OnValueChanged(long newValue)
    {
        Value = newValue;
        await ValueChanged.InvokeAsync(newValue);
    }
    
    #endregion
}