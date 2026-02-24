using Worker_SAP.Model;

namespace Worker_SAP.Repository.Sap.SalesOrdersRepositories
{
    public interface ISalesOrderRepository
    {
        void ConfigurarSessao(string sessionId);
        Task<bool> VerificarExistenciaOrder(string itemCode);
        Task<bool> AdicionarOrderAsync(SalesOrder item);
    }
}
