using Microsoft.Extensions.Options;
using NS.Notifications.Infrastructure.Configuration;
using RabbitMQ.Client;

namespace NS.Notifications.Infrastructure.Messaging;

public class RabbitMqConnectionFactory
{
    private readonly RabbitMqSettings _settings;

    public RabbitMqConnectionFactory(IOptions<RabbitMqSettings> settings)
    {
        _settings = settings.Value;
    }

    public IConnection CreateConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            Port = _settings.Port,
            UserName = _settings.Username,
            Password = _settings.Password
        };
        return factory.CreateConnection();
    }
}
