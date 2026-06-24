namespace NS.Notifications.Infrastructure.Messaging;

public static class RabbitMqConstants
{
    public const string Exchange = "notifications";
    public const string EmailQueue = "queue.email";
    public const string SmsQueue = "queue.sms";
    public const string PushQueue = "queue.push";
}
