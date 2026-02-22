using Worker_SAP;
using Worker_SAP.Model;
using Worker_SAP.Repository.Csv;
using Worker_SAP.Repository.Sap.ItemSAP;
using Worker_SAP.Service.AuthService;
using Worker_SAP.Service.Csv;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddMemoryCache();

builder.Services.Configure<LoginRequest>(builder.Configuration.GetSection("SAPLogin"));

builder.Services.AddSingleton<ICsvRepository, CsvRepository>();
builder.Services.AddSingleton<ICsvService, CsvService>();
builder.Services.AddSingleton<IItemSAPRepository, ItemRepository>();
builder.Services.AddSingleton<IAuthService, AuthService>();

var host = builder.Build();
host.Run();
