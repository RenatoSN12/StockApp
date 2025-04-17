using MudBlazor;

namespace StockApp.Web;

public static class Configuration
{
    public const string HttpClientName = "StockApp";
    public static string BackendUrl { get; set; } = string.Empty;

    public static MudTheme Theme = new()
    {
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Quicksand", "sans-serif"]
            }
        },
        PaletteLight = new PaletteLight
        {
            Background = Colors.Gray.Lighten4,
        }
    };
}