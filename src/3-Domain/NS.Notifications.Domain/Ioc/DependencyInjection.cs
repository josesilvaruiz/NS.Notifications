using Microsoft.Extensions.DependencyInjection;

namespace NS.Notifications.Domain.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}
