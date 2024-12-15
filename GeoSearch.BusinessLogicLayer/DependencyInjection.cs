using GeoSearch.BusinessLogicLayer.ExternalServices;
using GeoSearch.BusinessLogicLayer.ServiceContracts;
using GeoSearch.BusinessLogicLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GeoSearch.BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILocationApiWrapper, FourSquareApiWrapper>();
        services.AddScoped<ILocationSearchService, LocationSearchService>();
        
        return services;
    }
}