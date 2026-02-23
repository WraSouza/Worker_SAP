using Worker_SAP.Model;

namespace Worker_SAP.Repository.Sap.ItemSAP
{
    public class ItemRepository(HttpClient httpClient) : BaseSAPRepository(httpClient), IItemSAPRepository
    {
        public Task<bool> AdicionarItemAsync(Item item, string sessionId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VerificarExistenciaItem(string itemCode)
        => await ExisteNoSapAsync($"https://linux-7lxj:50000/b1s/v1/Items('{itemCode}')");
    }
}
