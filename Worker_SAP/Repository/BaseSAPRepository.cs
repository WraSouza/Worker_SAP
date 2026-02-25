using System.Net.Http.Json;
using System.Text.Json;

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

            if (!response.IsSuccessStatusCode)
                return false;

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
                return false;

            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                // 🔹 Se existir propriedade "value"
                if (root.TryGetProperty("value", out var value))
                {
                    if (value.ValueKind == JsonValueKind.Array)
                        return value.GetArrayLength() > 0;

                    if (value.ValueKind == JsonValueKind.String)
                        return false;

                    return false;
                }

                // 🔹 Só entra aqui se NÃO existir "value"
                if (root.ValueKind == JsonValueKind.Object &&
                    root.EnumerateObject().Any())
                {
                    return true;
                }

                return false;
            }
            catch (JsonException)
            {
                return false;
            }

        }
        protected async Task<bool> PostarNoSapAsync<T>(string endpoint, T dados)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, dados);

            return response.IsSuccessStatusCode;
        }
    }
}
