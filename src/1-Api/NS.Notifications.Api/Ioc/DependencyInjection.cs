using NS.Notifications.Api.Extensions;
using NS.Notifications.App.Impl.Ioc;
using NS.Notifications.Infrastructure.Ioc;

namespace NS.Notifications.Api.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiServices();
        services.AddAppImpl();
        services.AddInfrastructure(configuration);
        return services;
    }
}
