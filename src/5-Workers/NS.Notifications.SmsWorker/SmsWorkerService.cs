using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NS.Notifications.SmsWorker.Ioc;

namespace NS.Notifications.SmsWorker;

public class SmsWorkerService : BackgroundService
{
    private readonly RabbitMqSettings _settings;
    private readonly ILogger<SmsWorkerService> _logger;

    public SmsWorkerService(IOptions<RabbitMqSettings> settings, ILogger<SmsWorkerService> logger)
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
