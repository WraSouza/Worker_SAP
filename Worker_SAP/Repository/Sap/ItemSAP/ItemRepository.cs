using Worker_SAP.Model;

namespace Worker_SAP.Repository.Sap.ItemSAP
{
    public class ItemRepository : IItemSAPRepository
    {
        public bool AdicionarItemAsync(Item item, string sessionId)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistenciaItem(string itemCode)
        {
            throw new NotImplementedException();
        }
    }
}
