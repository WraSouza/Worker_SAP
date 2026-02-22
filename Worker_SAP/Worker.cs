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
                try
                {
                    var arquivos = Directory.GetFiles(_inbox, "*.csv");

                    foreach (var arquivo in arquivos)
                    {
                        await service.ProcessarArquivoAsync(arquivo);
                    }                    

                }catch (Exception ex)
                {

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
