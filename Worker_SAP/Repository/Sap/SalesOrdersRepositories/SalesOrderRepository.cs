using Worker_SAP.Model;

namespace Worker_SAP.Repository.Sap.SalesOrdersRepositories
{
    public class SalesOrderRepository(HttpClient httpClient) : BaseSAPRepository(httpClient), ISalesOrderRepository
    {
        public async Task<bool> AdicionarOrderAsync(SalesOrder item)
         => await PostarNoSapAsync($"https://linux-7lxj:50000/b1s/v1/Orders", item);

        public async Task<bool> VerificarExistenciaOrder(string docEntry)
        => await ExisteNoSapAsync($"https://linux-7lxj:50000/b1s/v1/Orders?$filter=NumAtCard eq '{docEntry}'");
    }
}
