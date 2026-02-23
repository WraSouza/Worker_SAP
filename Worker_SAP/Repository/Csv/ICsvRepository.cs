using Worker_SAP.Model;

namespace Worker_SAP.Repository.Csv
{
    public interface ICsvRepository
    {       
        List<T> LerRegistros<T>(string caminhoArquivo) where T : class;
    }
}
