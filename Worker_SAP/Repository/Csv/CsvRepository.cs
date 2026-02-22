using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using Worker_SAP.Model;

namespace Worker_SAP.Repository.Csv
{
    public class CsvRepository : ICsvRepository
    {      

        public List<Item> LerRegistros(string caminhoArquivo)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";", 
                PrepareHeaderForMatch = args => args.Header.ToLower() 
            };
            
            using var reader = new StreamReader(caminhoArquivo);
            using var csv = new CsvReader(reader, config);

            return csv.GetRecords<Item>().ToList();            
        }
    }
}
