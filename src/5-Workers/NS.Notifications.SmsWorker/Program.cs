using NS.Notifications.SmsWorker.Ioc;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSmsWorkerServices(builder.Configuration);

var host = builder.Build();
host.Run();
