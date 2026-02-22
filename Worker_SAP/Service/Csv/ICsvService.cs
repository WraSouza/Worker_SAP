namespace Worker_SAP.Service.Csv
{
    public interface ICsvService
    {
        Task ProcessarArquivoAsync(string caminhoArquivo);
    }
}
