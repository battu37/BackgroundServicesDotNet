using EventDrivenOrderProcessor;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<MessageQueueService>();
builder.Services.AddSingleton<DatabaseService>();
builder.Services.AddHostedService<OrderProcessorService>();

var host = builder.Build();
host.Run();
