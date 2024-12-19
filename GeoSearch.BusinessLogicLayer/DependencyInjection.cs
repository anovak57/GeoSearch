using FluentValidation;
using GeoSearch.BusinessLogicLayer.ExternalServices;
using GeoSearch.BusinessLogicLayer.ServiceContracts;
using GeoSearch.BusinessLogicLayer.Services;
using GeoSearch.BusinessLogicLayer.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace GeoSearch.BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILocationApiWrapper, FourSquareApiWrapper>();
        services.AddHttpClient<FourSquareApiWrapper>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddValidatorsFromAssemblyContaining<LocationSearchRequestValidator>();
        return services;
    }
}