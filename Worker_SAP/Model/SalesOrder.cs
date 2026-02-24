using CsvHelper.Configuration.Attributes;
using System.Text.Json.Serialization;

namespace Worker_SAP.Model
{
    public class SalesOrder
    {
        public SalesOrder(string numero, DateOnly dataLancamento, DateOnly dataDocumento, DateOnly dataVencimento, string cardCode)
        {
            NumAtCard = numero;
            DocDate = dataLancamento;
            TaxDate = dataDocumento;
            DocDueDate = dataVencimento;
            CardCode = cardCode;                       
            DocumentLines = new List<DocumentLine>();           
        }

        [JsonPropertyName("NumAtCard")]
        public string NumAtCard { get; set; }
        [Format("yyyy-MM-dd")]
        [JsonPropertyName("DocDate")]
        public DateOnly DocDate { get; set; }
        [Format("yyyy-MM-dd")]
        [JsonPropertyName("TaxDate")]
        public DateOnly TaxDate { get; set; }
        [Format("yyyy-MM-dd")]
        [JsonPropertyName("DocDueDate")]
        public DateOnly DocDueDate { get; set; }
        [JsonPropertyName("CardCode")]
        public string CardCode { get; set; }        
        [JsonPropertyName("DocumentLines")]
        public List<DocumentLine> DocumentLines { get; set; }

        public void AddDocumentLine(DocumentLine line)
        {
            DocumentLines.Add(line);
        }


    }
}
