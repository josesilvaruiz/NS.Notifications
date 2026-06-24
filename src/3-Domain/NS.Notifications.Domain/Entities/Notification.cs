namespace NS.Notifications.Domain.Entities;

public class Notification
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Channel { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
