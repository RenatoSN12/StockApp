using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockApp.Application.DTOs.Responses.Location;
using StockApp.Web.Extensions;
using StockApp.Web.Services.Abstractions;

namespace StockApp.Web.Pages.Location;

public partial class ListLocationsPage : ComponentBase
{
    #region Fields
    
    protected List<LocationDto> Locations { get; set; } = [];
    protected string SearchTerm { get; set; } = string.Empty;

    #endregion
    
    #region Services

    [Inject] public ILocationService LocationService { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    
    #endregion
    
    #region Methods

    protected override async Task OnInitializedAsync()
    {
        var result = await LocationService.GetAllAsync();
        
        if (result.IsFailure)
        {
            foreach (var error in result.Error.Message.SplitErrors())
            {
                Snackbar.Add(error, Severity.Error);
            }
        }
        Locations = result.Value!.Data ?? [];
    }
    
    public Func<LocationDto, bool> Filter => location =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;
    
        if (location.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        
        if (location.Title.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
    
        if (location.Description.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    };

    #endregion
}