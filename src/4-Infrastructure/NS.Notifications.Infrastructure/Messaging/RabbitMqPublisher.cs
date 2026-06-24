using Microsoft.Extensions.Logging;
using NS.Notifications.Domain.Contracts;
using NS.Notifications.Domain.Entities;

namespace NS.Notifications.Infrastructure.Messaging;

public class RabbitMqPublisher : INotificationPublisher, IDisposable
{
    private readonly ILogger<RabbitMqPublisher> _logger;

    public RabbitMqPublisher(RabbitMqConnectionFactory connectionFactory, ILogger<RabbitMqPublisher> logger)
    {
        _logger = logger;
    }

    public Task PublishAsync(Notification notification)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
