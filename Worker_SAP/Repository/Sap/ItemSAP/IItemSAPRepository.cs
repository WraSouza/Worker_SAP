using Worker_SAP.Model;

namespace Worker_SAP.Repository.Sap.ItemSAP
{
    public interface IItemSAPRepository
    {
        bool VerificarExistenciaItem(string itemCode);
        bool AdicionarItemAsync(Item item, string sessionId);
    }
}
