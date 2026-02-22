using Worker_SAP.Model;

namespace Worker_SAP.Repository.Csv
{
    public interface ICsvRepository
    {
        List<Item> LerRegistros(string caminhoArquivo);
    }
}
