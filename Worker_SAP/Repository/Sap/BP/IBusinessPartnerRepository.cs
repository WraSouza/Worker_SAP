using Worker_SAP.Model;

namespace Worker_SAP.Repository.Sap.BP
{
    public interface IBusinessPartnerRepository
    {
        Task<bool> VerificarExistenciaBPAsync(string cardCode);
        Task<bool> AdicionarBPAsync(BusinessPartner businessPartner, string sessionId);
    }
}
