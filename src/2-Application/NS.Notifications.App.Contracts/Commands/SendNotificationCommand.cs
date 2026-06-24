using MediatR;
using NS.Notifications.App.Contracts.DTOs;
using NS.Notifications.App.Contracts.Enums;

namespace NS.Notifications.App.Contracts.Commands;

public record SendNotificationCommand(string UserId, string Message, NotificationChannel Channel) : IRequest<NotificationDto>;
