using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NS.Notifications.EmailWorker.Configuration;

namespace NS.Notifications.EmailWorker.Ioc;

public class RabbitMqSettings
{
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 5672;
    public string Username { get; set; } = "guest";
    public string Password { get; set; } = "guest";
}

public static class DependencyInjection
{
    public static IServiceCollection AddEmailWorkerServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<RabbitMqSettings>(config.GetSection("RabbitMq"));
        services.Configure<SmtpSettings>(config.GetSection("Smtp"));
        services.AddHostedService<EmailWorkerService>();
        return services;
    }
}
