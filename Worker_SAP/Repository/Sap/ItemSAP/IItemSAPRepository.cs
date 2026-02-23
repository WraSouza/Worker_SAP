using Worker_SAP.Model;

namespace Worker_SAP.Repository.Sap.ItemSAP
{
    public interface IItemSAPRepository
    {
        void ConfigurarSessao(string sessionId);
        Task<bool> VerificarExistenciaItem(string itemCode);
        Task<bool> AdicionarItemAsync(Item item, string sessionId);
    }
}
