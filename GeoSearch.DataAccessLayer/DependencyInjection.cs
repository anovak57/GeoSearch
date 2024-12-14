using GeoSearch.DataAccessLayer.Context;
using GeoSearch.DataAccessLayer.Repositories;
using GeoSearch.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeoSearch.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
        });
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}