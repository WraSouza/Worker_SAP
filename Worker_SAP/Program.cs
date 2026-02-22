using Worker_SAP;
using Worker_SAP.Repository.Csv;
using Worker_SAP.Service.Csv;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<ICsvRepository, CsvRepository>();
builder.Services.AddSingleton<ICsvService, CsvService>();

var host = builder.Build();
host.Run();
