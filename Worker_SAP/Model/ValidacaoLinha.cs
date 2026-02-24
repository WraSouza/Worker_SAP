namespace Worker_SAP.Model
{
    public class ValidacaoLinha
    {
        public ValidacaoLinha(string numero, DateTime dataLancamento, DateTime dataDocumenti, DateTime dataVencimento, string cardCode)
        {
            Numero = numero;
            DataLancamento = dataLancamento;
            DataDocumento = dataDocumenti;
            DataVencimento = dataVencimento;
            CardCode = cardCode;
        }

        public string Numero { get; private set; }
        public DateTime DataLancamento { get; private set; }
        public DateTime DataDocumento { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public string CardCode { get; private set; }
    }
}
