using MediatR;
using Microsoft.Extensions.Logging;
using NS.Notifications.App.Contracts.Commands;
using NS.Notifications.App.Contracts.DTOs;
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

    public Task<NotificationDto> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
