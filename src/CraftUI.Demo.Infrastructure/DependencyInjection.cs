using CraftUI.Demo.Application.Common.Interfaces.Infrastructure;
using CraftUI.Demo.Infrastructure.Displays;
using CraftUI.Demo.Infrastructure.Navigation;
using CraftUI.Demo.Infrastructure.Toasts;
using Microsoft.Extensions.DependencyInjection;

namespace CraftUI.Demo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IToastService, ToastService>();
        services.AddSingleton<IDisplayService, DisplayService>();
        
        return services;
    }
}
