using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockApp.Application.DTOs.Requests.ProductStock;
using StockApp.Application.DTOs.Responses.Location;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.DTOs.Responses.ProductStock;
using StockApp.Domain.Entities;
using StockApp.Web.Extensions;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Pages.Products;

public partial class DetailProductPage : ComponentBase
{
    #region Fields

    [EditorRequired] public ProductDto Product { get; set; } = new();

    [Parameter] public string CustomId { get; set; } = string.Empty;

    public Dictionary<long, string> AvailableLocations { get; set; } = [];
    public List<ProductStockDto> Locations => Product.Locations ?? [];
    public long SelectedLocationId { get; set; }
    public string SelectedLocationTitle => AvailableLocations.GetValueOrDefault(SelectedLocationId) ?? string.Empty;
    public int NewProductStockMaxQuantity { get; set; }
    public int NewProductStockMinQuantity { get; set; }

    #endregion

    #region Services

    [Inject] private IProductService ProductService { get; set; } = null!;
    [Inject] private ICategoryService CategoryService { get; set; } = null!;
    [Inject] private IProductStockService ProductStockService { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
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
            if (!Locations.Select(x => x.LocationId).Contains(location.Id))
                locations.Add(location);
        }

        AvailableLocations = locations.ToDictionary(x => x.Id, x => x.Title);
    }

    protected async Task AddLocation(long locationId, string locationTitle, int minQuantity, int maxQuantity)
    {
        if (SelectedLocationId == 0)
            return;

        var result = await ProductStockService.AddAsync(new CreateProductStockDto(Product.Id, locationId, maxQuantity, minQuantity));
        if (result.IsSuccess)
        {
            Snackbar.Add($"Adicionado controle de estoque para o item {Product.Title} no local {locationTitle}.", Severity.Success);
            Locations.Add(result.Value!);
            StateHasChanged();
        }
        else
        {
            foreach (var error in result.Error.Message.SplitErrors())
                Snackbar.Add(error, Severity.Error);
        }
    }

    protected async Task OnDeleteProductStockButtonClicked(long locationId, string locationTitle)
    {
        var result = await DialogService.ShowMessageBox(
            "ATENÇÃO",
            $"Deseja realmente parar de controlar o estoque do item {CustomId} - {Product.Title} no estoque {locationId} - {locationTitle}?",
            yesText: "SIM", cancelText: "NÃO");

        if (result is not null && result == true)
            await DeleteProductStock(Product.Id,locationId);
    }

    private async Task DeleteProductStock(long productId, long locationId)
    {
        var result = await ProductStockService.DeleteAsync(new DeleteProductStockDto(productId, locationId));

        if (result.IsSuccess)
        {
            Locations.RemoveAll(x => x.LocationId == locationId);
            Snackbar.Add(result.Value!, Severity.Success);
        }
        else
        {
            foreach (var error in result.Error.Message.SplitErrors())
                Snackbar.Add(error, Severity.Error);
        }
    }

    protected void Cancel() => NavigationManager.NavigateTo("/products");

    protected async Task SaveChanges()
    {
        var result = await ProductService.UpdateAsync(Product);
        if (result.IsFailure)
        {
            foreach (var erro in result.Error.Message.SplitErrors())
                Snackbar.Add(erro, Severity.Error);
        }
        else
        {
            Snackbar.Add("Produto atualizado com sucesso!", Severity.Success);
            NavigationManager.NavigateTo("/products");
        }
    }

    #endregion
}