using NS.Notifications.EmailWorker.Ioc;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddEmailWorkerServices(builder.Configuration);

var host = builder.Build();
host.Run();
