using Microsoft.AspNetCore.Authentication.Cookies;
using StockApp.Application;
using StockApp.Domain;
using StockApp.Infrastructure;

namespace StockApp.Api.Common.Extensions;

public static class ServicesExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddAuthorization();
        services.AddApplication();
        services.AddInfrastructure();
    }
    public static void AddAuthenticationSetup(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/auth/login";
                options.LogoutPath = "/auth/logout";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                options.SlidingExpiration = true;
            });
    }

    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(n => n.FullName);
        });
    }
    
    public static void AddCrossOrigin(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy(ApiConfiguration.CorsPolicyName,
            policy => policy
                .WithOrigins([Configuration.BackendUrl, Configuration.FrontendUrl])
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()));
    }
}
