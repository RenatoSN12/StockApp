using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockApp.Application.DTOs.Responses.Location;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Domain.Entities;
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
    
    public Dictionary<long, string> AvailableLocations { get; set; } = [];
    public List<ProductLocationDto> Locations => Product.Locations ?? [];
    public long SelectedLocationId { get; set; }
    public decimal NewQuantity { get; set; }


    #endregion

    #region Services
    
    [Inject] private IProductService ProductService { get; set; } = null!;
    [Inject] private ICategoryService CategoryService { get; set; } = null!;
    [Inject] private ILocationService LocationService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
    
    #endregion
    
    #region Overrides
    
    protected override async Task OnParametersSetAsync()
    {
        await GetProduct();
        await GetAvailableLocations();
        StateHasChanged();
    }

    #endregion
    
    #region Methods

    private async Task GetProduct()
    {
        var result = await ProductService.GetByCustomId(CustomId);
        if (result is { IsSuccess: true, Value: not null })
        {
            Product = result.Value;
        }
    }

    private async Task GetAvailableLocations()
    {
        var result = await LocationService.GetAllAsync();
        List<LocationDto> locations = [];
        
        foreach (var location in result.Value!.Data!)
        {
            if(!Locations.Select(x=> x.LocationId).Contains(location.Id))
                locations.Add(location);
        }
        
        AvailableLocations = locations.ToDictionary(x=>x.Id, x=>x.Title);
    }

    protected void AddLocation()
    {
        // if (SelectedLocationId == 0 || Locations.Any(x => x.LocationId == SelectedLocationId))
        //     return;
        //
        // var location = AvailableLocations.First(x => x.Id == SelectedLocationId);
        // Locations.Add(new ProductLocationDto
        // {
        //     LocationId = location.Id,
        //     LocationName = location.Title,
        //     Quantity = NewQuantity
        // });
    }
    
    protected void RemoveLocation(long locationId)
    {
        Locations.RemoveAll(x => x.LocationId == locationId);
    }

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