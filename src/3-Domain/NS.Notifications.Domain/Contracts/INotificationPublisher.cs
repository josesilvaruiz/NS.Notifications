using NS.Notifications.Domain.Entities;

namespace NS.Notifications.Domain.Contracts;

public interface INotificationPublisher
{
    Task PublishAsync(Notification notification);
}
