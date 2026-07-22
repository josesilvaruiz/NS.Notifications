using Microsoft.Extensions.Logging;
using NS.Notifications.Domain.Contracts;
using NS.Notifications.Domain.Entities;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace NS.Notifications.Infrastructure.Messaging;

public class RabbitMqPublisher : INotificationPublisher, IDisposable
{
    private readonly ILogger<RabbitMqPublisher> _logger;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqPublisher(RabbitMqConnectionFactory connectionFactory, ILogger<RabbitMqPublisher> logger)
    {
        _logger = logger;

        // 1. Abrimos UNA conexión TCP con el broker (cara de crear, se reutiliza)
        _connection = connectionFactory.CreateConnection();

        // 2. Abrimos UN canal sobre esa conexión (ligero, es por donde mandamos comandos)
        _channel = _connection.CreateModel();

        // 3. Declaramos el exchange. Tipo "direct" = enruta por coincidencia exacta de routing key.
        //    Es idempotente: si ya existe con esta config, no pasa nada.
        _channel.ExchangeDeclare(
            exchange: RabbitMqConstants.Exchange,
            type: ExchangeType.Direct,
            durable: true); // durable = sobrevive a un reinicio de RabbitMQ

        // 4. Declaramos cada cola y la "atamos" (bind) al exchange con su routing key.
        //    Esto es lo que decide: mensaje con routing key "email" -> cae en queue.email
        DeclareAndBind(RabbitMqConstants.EmailQueue, "email");
        DeclareAndBind(RabbitMqConstants.SmsQueue, "sms");
        DeclareAndBind(RabbitMqConstants.PushQueue, "push");
    }

    private void DeclareAndBind(string queueName, string routingKey)
    {
        _channel.QueueDeclare(
            queue: queueName,
            durable: true,      // la cola sobrevive a un reinicio del broker
            exclusive: false,   // varios consumidores pueden conectarse a ella
            autoDelete: false); // no se borra sola cuando no hay consumidores

        _channel.QueueBind(
            queue: queueName,
            exchange: RabbitMqConstants.Exchange,
            routingKey: routingKey);
    }

    public Task PublishAsync(Notification notification)
    {
        // El canal no entiende objetos C#, solo bytes. Serializamos a JSON y lo convertimos a byte[].
        var json = JsonSerializer.Serialize(notification);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = _channel.CreateBasicProperties();
        properties.Persistent = true; // el mensaje se guarda en disco, no solo en memoria

        // Aquí está la clave: publicamos AL EXCHANGE, con la routing key = el canal de la notificación.
        // No publicamos directo a la cola — es el exchange quien decide a dónde va según el binding.
        _channel.BasicPublish(
            exchange: RabbitMqConstants.Exchange,
            routingKey: notification.Channel,
            basicProperties: properties,
            body: body);

        _logger.LogInformation(
            "Notificación {Id} publicada con routing key '{Channel}'",
            notification.Id, notification.Channel);

        // BasicPublish en v6 es síncrono (fire-and-forget desde el punto de vista de la API),
        // por eso devolvemos un Task ya completado en vez de awaitear algo.
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}
