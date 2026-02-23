using System.Net.Http.Json;

namespace Worker_SAP.Repository
{
    public abstract class BaseSAPRepository(HttpClient httpClient)
    {
        protected readonly HttpClient _httpClient = httpClient;

        // Centraliza a configuração do Cookie do SAP
        public void ConfigurarSessao(string sessionId)
        {
            // Remove se já existir para não duplicar headers
            _httpClient.DefaultRequestHeaders.Remove("Cookie");
           
            _httpClient.DefaultRequestHeaders.Add("Cookie", $"B1SESSION={sessionId}; ROUTEID=.node1");
        }

        protected async Task<bool> ExisteNoSapAsync(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            return response.IsSuccessStatusCode;
        }

        protected async Task<bool> PostarNoSapAsync<T>(string endpoint, T dados)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, dados);
            return response.IsSuccessStatusCode;
        }
    }
}
