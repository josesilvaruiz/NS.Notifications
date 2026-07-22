namespace NS.Notifications.EmailWorker.Configuration;

public class ResendSettings
{
    public string ApiKey { get; set; } = string.Empty;

    // Sandbox por defecto: solo llega al email con el que te registraste en Resend.
    // Para enviar a destinatarios reales hace falta un dominio verificado en Resend
    // y cambiar esto a algo como "alertas@tudominio.com".
    public string From { get; set; } = "onboarding@resend.dev";
    public string FromDisplayName { get; set; } = "NS Notifications";

    // Coma-separado en vez de un array: se puede fijar con una sola env var.
    public string Recipients { get; set; } = string.Empty;

    public IReadOnlyList<string> RecipientList =>
        Recipients.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
}
