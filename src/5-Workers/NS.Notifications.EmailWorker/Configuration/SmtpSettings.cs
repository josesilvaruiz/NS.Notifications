namespace NS.Notifications.EmailWorker.Configuration;

public class SmtpSettings
{
    public string Host { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromDisplayName { get; set; } = "NS Notifications";

    // Coma-separado en vez de un array: así se puede fijar con una sola env var
    // (Smtp__Recipients) en vez de tener que indexar Smtp__Recipients__0, __1, etc.
    public string Recipients { get; set; } = string.Empty;

    public IReadOnlyList<string> RecipientList =>
        Recipients.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
}
