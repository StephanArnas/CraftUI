using CraftUI.Demo.Application.Common.Interfaces.Services;
using CraftUI.Demo.Services.Cities;
using CraftUI.Demo.Services.Countries;
using Microsoft.Extensions.DependencyInjection;

namespace CraftUI.Demo.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<ICountryService, CountryService>();
        services.AddSingleton<ICityService, CityService>();

        return services;
    }
}
