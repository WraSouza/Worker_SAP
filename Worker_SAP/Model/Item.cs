using System.Text.Json.Serialization;

namespace Worker_SAP.Model
{
    public class Item
    {
        public Item(string itemCode, string itemName)
        {
            ItemCode = itemCode;
            ItemName = itemName;
            ItemType = "itItems";
            SalesUnit = "UN";
        }

        [JsonPropertyName("ItemCode")]
        public string ItemCode { get;  private set; }
        [JsonPropertyName("ItemName")]
        public string ItemName { get;  private set; }
        [JsonPropertyName("SalesUnit")]
        public string SalesUnit { get; private set; }
        [JsonPropertyName("ItemType")]
        public string ItemType { get; private set; }
    }
}
