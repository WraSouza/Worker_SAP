using Worker_SAP.Model;

namespace Worker_SAP.Repository.Sap.BP
{
    internal class BusinessPartnerRepository(HttpClient httpClient) : BaseSAPRepository(httpClient), IBusinessPartnerRepository
    {
        public async Task<bool> AdicionarBPAsync(BusinessPartner businessPartner)
       => await PostarNoSapAsync($"https://linux-7lxj:50000/b1s/v1/BusinessPartners", businessPartner);

        public async Task<bool> VerificarExistenciaBPAsync(string cardCode)
         => await ExisteNoSapAsync($"https://linux-7lxj:50000/b1s/v1/BusinessPartners('{cardCode}')");
    }
}
