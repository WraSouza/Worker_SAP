using Worker_SAP;
using Worker_SAP.Model;
using Worker_SAP.Repository.Csv;
using Worker_SAP.Repository.Sap.BP;
using Worker_SAP.Repository.Sap.ItemSAP;
using Worker_SAP.Repository.Sap.SalesOrdersRepositories;
using Worker_SAP.Service.AuthService;
using Worker_SAP.Service.Csv;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddMemoryCache();

builder.Services.AddWindowsService();

builder.Services.Configure<LoginRequest>(builder.Configuration.GetSection("SAPLogin"));

builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<IBusinessPartnerRepository, BusinessPartnerRepository>();
builder.Services.AddSingleton<ICsvRepository, CsvRepository>();
builder.Services.AddSingleton<ICsvService, CsvService>();
builder.Services.AddSingleton<IItemSAPRepository, ItemRepository>();
builder.Services.AddSingleton<IAuthService, AuthService>();

builder.Services.AddHttpClient<IItemSAPRepository, ItemRepository>(client =>
{
    client.BaseAddress = new Uri("https://linux-7lxj:50000/b1s/v1/");
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
});

builder.Services.AddHttpClient<IBusinessPartnerRepository, BusinessPartnerRepository>(client =>
{
    client.BaseAddress = new Uri("https://linux-7lxj:50000/b1s/v1/");
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
});

builder.Services.AddHttpClient<ISalesOrderRepository, SalesOrderRepository>(client =>
{
    client.BaseAddress = new Uri("https://linux-7lxj:50000/b1s/v1/");
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
});

var host = builder.Build();
host.Run();
