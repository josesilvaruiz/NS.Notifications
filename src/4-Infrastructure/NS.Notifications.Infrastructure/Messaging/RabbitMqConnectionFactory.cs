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
        throw new NotImplementedException();
    }
}
