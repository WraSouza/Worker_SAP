namespace Worker_SAP.Model
{
    public class Item
    {
        public Item(string itemCode, string itemName, string unidadeMedida)
        {
            ItemCode = itemCode;
            ItemName = itemName;
            UnidadeMedida = unidadeMedida;
        }

        public string ItemCode { get;  set; }
        public string ItemName { get;  set; }
        public string UnidadeMedida { get; set; }
    }
}
