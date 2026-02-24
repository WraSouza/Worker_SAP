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
            {

                return false;
            }

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);

            if (!doc.RootElement.TryGetProperty("value", out var value))
                return false;

            return value.GetArrayLength() > 0;
        }

        //protected async Task<bool> ExisteNoSapAsync(string endpoint)
        //{
        //    bool existe = true;
        //    var response = await _httpClient.GetAsync(endpoint);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return false;
        //        // Lê o erro detalhado que o servidor enviou no corpo da resposta
        //        var conteudoErro = await response.Content.ReadAsStringAsync();

        //        // Loga ou imprime para você conseguir debugar
        //        Console.WriteLine($"Erro: {response.StatusCode}");
        //        Console.WriteLine($"Detalhes: {conteudoErro}");

        //        // Opcional: lança uma exceção com o detalhe
        //        // response.EnsureSuccessStatusCode(); 
        //    }

        //    var json = await response.Content.ReadAsStringAsync();

        //    using var doc = JsonDocument.Parse(json);

        //    var value = doc.RootElement.GetProperty("value");

        //    if(value.GetArrayLength() > 0)
        //        return existe;

        //    //return response.IsSuccessStatusCode;
        //}

        protected async Task<bool> PostarNoSapAsync<T>(string endpoint, T dados)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, dados);

            return response.IsSuccessStatusCode;
        }
    }
}
