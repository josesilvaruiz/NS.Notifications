using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NS.Notifications.SmsWorker.Ioc;

public class RabbitMqSettings
{
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 5672;
    public string Username { get; set; } = "guest";
    public string Password { get; set; } = "guest";
}

public static class DependencyInjection
{
    public static IServiceCollection AddSmsWorkerServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<RabbitMqSettings>(config.GetSection("RabbitMq"));
        services.AddHostedService<SmsWorkerService>();
        return services;
    }
}
