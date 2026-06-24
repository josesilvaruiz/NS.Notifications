using NS.Notifications.Domain.Enums;

namespace NS.Notifications.App.Contracts.DTOs;

public record NotificationDto(Guid Id, NotificationStatus Status, DateTime SentAt);
