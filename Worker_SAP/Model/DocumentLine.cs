using System.Text.Json.Serialization;

namespace Worker_SAP.Model
{
    public class DocumentLine
    {
        public DocumentLine(string itemCode, string quantidade, string precoUnitario)
        {
            ItemCode = itemCode;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            
        }

        [JsonPropertyName("ItemCode")]
        public string ItemCode { get; private set; }

        [JsonPropertyName("Quantity")]
        public string Quantidade { get; private set; }

        [JsonPropertyName("Price")]
        public string PrecoUnitario { get; private set; }
       
    }
}
