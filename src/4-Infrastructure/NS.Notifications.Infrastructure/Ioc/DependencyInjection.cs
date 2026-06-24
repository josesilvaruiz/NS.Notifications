using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NS.Notifications.Domain.Contracts;
using NS.Notifications.Domain.Ioc;
using NS.Notifications.Infrastructure.Configuration;
using NS.Notifications.Infrastructure.Messaging;

namespace NS.Notifications.Infrastructure.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDomain();
        services.Configure<RabbitMqSettings>(config.GetSection("RabbitMq"));
        services.AddSingleton<RabbitMqConnectionFactory>();
        services.AddSingleton<INotificationPublisher, RabbitMqPublisher>();
        return services;
    }
}
