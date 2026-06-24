using Microsoft.Extensions.DependencyInjection;
using NS.Notifications.App.Contracts.Ioc;
using NS.Notifications.App.Impl.Extensions;

namespace NS.Notifications.App.Impl.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddAppImpl(this IServiceCollection services)
    {
        services.AddApplicationServices();
        services.AddAppContracts();
        return services;
    }
}
