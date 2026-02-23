using Worker_SAP.Model;

namespace Worker_SAP.Repository.Sap.ItemSAP
{
    public class ItemRepository(HttpClient httpClient) : BaseSAPRepository(httpClient), IItemSAPRepository
    {
        public async Task<bool> AdicionarItemAsync(Item item)
        => await PostarNoSapAsync($"https://linux-7lxj:50000/b1s/v1/Items", item);

        public async Task<bool> VerificarExistenciaItem(string itemCode)
        => await ExisteNoSapAsync($"https://linux-7lxj:50000/b1s/v2/Items('{itemCode}')");
    }
}
