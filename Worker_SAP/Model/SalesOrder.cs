namespace Worker_SAP.Model
{
    public class SalesOrder
    {
        public SalesOrder(string numero, DateOnly dataLancamento, DateOnly dataDocumento, DateOnly dataVencimento, string cardCode, string itemCode, string precoUnitario, string quantidade)
        {
            Numero = numero;
            DataLancamento = dataLancamento;
            DataDocumento = dataDocumento;
            DataVencimento = dataVencimento;
            CardCode = cardCode;
            ItemCode = itemCode;
            PrecoUnitario = precoUnitario;
            Quantidade = quantidade;
        }

        public string Numero { get; private set; }
        public DateOnly DataLancamento { get; private set; }
        public DateOnly DataDocumento { get; private set; }
        public DateOnly DataVencimento { get; private set; }
        public string CardCode { get; private set; }
        public string ItemCode { get; private set; }
        public string PrecoUnitario { get; private set; }
        public string Quantidade { get; private set; }
    }
}
