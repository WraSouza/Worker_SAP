namespace Worker_SAP.Model
{
    public class BusinessPartner
    {
        public BusinessPartner(string cardCode, string cardName, string tipoDocumento, string documento, string rua, string numero, string bairro, string cidade, string estado, string pais)
        {
            CardCode = cardCode;
            CardName = cardName;
            TipoDocumento = tipoDocumento;
            Documento = documento;
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
        }

        public string CardCode { get; private set; }
        public string CardName { get; private set; }
        public string TipoDocumento { get; private set; }
        public string Documento { get; private set; }
        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Pais { get; private set; }
    }
}
