using Microsoft.Extensions.DependencyInjection;
using StockApp.Application.Abstractions.Security;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Repositories;
using StockApp.Infrastructure.Data;
using StockApp.Infrastructure.Repositories;
using StockApp.Infrastructure.Services;

namespace StockApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IProductStockRepository, ProductStockRepository>();
        services.AddScoped<IMovementRepository, MovementRepository>();
        return services;
    }
}