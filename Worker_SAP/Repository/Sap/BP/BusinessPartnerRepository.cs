using Worker_SAP.Model;

namespace Worker_SAP.Repository.Sap.BP
{
    internal class BusinessPartnerRepository(HttpClient httpClient) : BaseSAPRepository(httpClient), IBusinessPartnerRepository
    {
        public Task<bool> AdicionarBPAsync(BusinessPartner businessPartner, string sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerificarExistenciaBPAsync(string cardCode)
        {
            throw new NotImplementedException();
        }
    }
}
