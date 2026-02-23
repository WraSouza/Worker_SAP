using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Worker_SAP.Repository.Csv
{
    public class CsvRepository : ICsvRepository
    {
        public List<T> LerRegistros<T>(string caminhoArquivo) where T : class
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                PrepareHeaderForMatch = args => args.Header.ToLower()
            };

            using var reader = new StreamReader(caminhoArquivo);
            using var csv = new CsvReader(reader, config);

            // O GetRecords agora usa o T genérico
            return csv.GetRecords<T>().ToList();
        }
       
    }
}
