using Automated_Report_GenerationWorkerService;

// refer /c/6714ccd6-b1b0-8013-b02b-240626eab467

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<DatabaseService>();
builder.Services.AddSingleton<FileService>();
builder.Services.AddHostedService<AutomatedReportGenerator>();

var host = builder.Build();
host.Run();
