using Worker_SAP.Service.Csv;

namespace Worker_SAP
{
    public class Worker(ILogger<Worker> logger, ICsvService service) : BackgroundService
    {
        private static readonly string _pastaRaiz = @"C:\Users\wladimir.souza\Documents\DesafioPowerOne";

        private static readonly string _inbox = Path.Combine(_pastaRaiz, "inbox");       

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
               
                    var arquivos = Directory.GetFiles(_inbox, "*.csv");

                    var arquivosOrdenados = arquivos.OrderBy(f =>
                    {
                        if (f.Contains("items", StringComparison.OrdinalIgnoreCase)) return 1;
                        if (f.Contains("businesspartners", StringComparison.OrdinalIgnoreCase)) return 2;
                        return 3;
                    });

                    foreach (var arquivo in arquivosOrdenados)
                    {                        
                        await service.ProcessarArquivoAsync(arquivo);                        

                    }                
              

                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
