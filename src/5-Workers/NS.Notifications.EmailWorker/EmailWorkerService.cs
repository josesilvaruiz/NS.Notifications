using Microsoft.Extensions.Options;
using NS.Notifications.EmailWorker.Ioc;

namespace NS.Notifications.EmailWorker;

public class EmailWorkerService : BackgroundService
{
    private readonly RabbitMqSettings _settings;
    private readonly ILogger<EmailWorkerService> _logger;

    public EmailWorkerService(IOptions<RabbitMqSettings> settings, ILogger<EmailWorkerService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
