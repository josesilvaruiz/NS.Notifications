using MediatR;
using Microsoft.Extensions.Logging;
using NS.Notifications.App.Contracts.Commands;
using NS.Notifications.App.Contracts.DTOs;
using NS.Notifications.Domain.Entities;
using NS.Notifications.Domain.Enums;
using DomainContracts = NS.Notifications.Domain.Contracts;

namespace NS.Notifications.App.Impl.Handlers;

public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, NotificationDto>
{
    private readonly DomainContracts.INotificationPublisher _publisher;
    private readonly ILogger<SendNotificationCommandHandler> _logger;

    public SendNotificationCommandHandler(DomainContracts.INotificationPublisher publisher, ILogger<SendNotificationCommandHandler> logger)
    {
        _publisher = publisher;
        _logger = logger;
    }

    public async Task<NotificationDto> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Message = request.Message,
            Channel = request.Channel.ToString().ToLowerInvariant(), // debe coincidir con las routing keys "email"/"sms"/"push"
            CreatedAt = DateTime.UtcNow
        };

        await _publisher.PublishAsync(notification);

        _logger.LogInformation("Notificación {Id} encolada para {UserId} vía {Channel}", notification.Id, notification.UserId, notification.Channel);

        return new NotificationDto(notification.Id, NotificationStatus.Sent, DateTime.UtcNow);
    }
}
