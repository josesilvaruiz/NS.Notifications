using System.Text;
using System.Text.Json;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using NS.Notifications.EmailWorker.Configuration;
using NS.Notifications.EmailWorker.Ioc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NS.Notifications.EmailWorker;

public class EmailWorkerService : BackgroundService
{
    private const string Exchange = "notifications";
    private const string Queue = "queue.email";
    private const string RoutingKey = "email";

    private readonly RabbitMqSettings _settings;
    private readonly SmtpSettings _smtpSettings;
    private readonly ILogger<EmailWorkerService> _logger;
    private IConnection? _connection;
    private IModel? _channel;

    public EmailWorkerService(IOptions<RabbitMqSettings> settings, IOptions<SmtpSettings> smtpSettings, ILogger<EmailWorkerService> logger)
    {
        _settings = settings.Value;
        _smtpSettings = smtpSettings.Value;
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            Port = _settings.Port,
            UserName = _settings.Username,
            Password = _settings.Password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(Exchange, ExchangeType.Direct, durable: true);
        _channel.QueueDeclare(Queue, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind(Queue, Exchange, RoutingKey);

        // Un mensaje en vuelo por consumidor: evita que se acumulen sin confirmar si el envío tarda.
        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (_, args) =>
        {
            var json = Encoding.UTF8.GetString(args.Body.ToArray());

            try
            {
                var notification = JsonSerializer.Deserialize<NotificationMessage>(json)
                    ?? throw new InvalidOperationException("Notificación deserializada a null");

                await SendEmailAsync(notification);

                _logger.LogInformation(
                    "Email enviado a {Recipients} por notificación de {UserId}",
                    string.Join(", ", _smtpSettings.RecipientList), notification.UserId);

                _channel!.BasicAck(args.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error procesando notificación de email, se descarta el mensaje");
                _channel!.BasicNack(args.DeliveryTag, multiple: false, requeue: false);
            }
        };

        _channel!.BasicConsume(Queue, autoAck: false, consumer);

        // El consumo real ocurre por eventos (consumer.Received); no hay loop que ejecutar aquí.
        return Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _channel?.Close();
        _connection?.Close();
        await base.StopAsync(cancellationToken);
    }

    private async Task SendEmailAsync(NotificationMessage notification)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_smtpSettings.FromDisplayName, _smtpSettings.Username));
        foreach (var recipient in _smtpSettings.RecipientList)
            email.To.Add(MailboxAddress.Parse(recipient));

        email.Subject = $"[NS Notifications] Alerta de {notification.UserId}";
        email.Body = new TextPart("plain")
        {
            Text = $"{notification.Message}\n\n(UserId: {notification.UserId}, generado el {notification.CreatedAt:u})"
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
        await client.SendAsync(email);
        await client.DisconnectAsync(quit: true);
    }
}

internal record NotificationMessage(Guid Id, string UserId, string Message, string Channel, DateTime CreatedAt);
