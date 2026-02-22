using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using Worker_SAP.Model;

namespace Worker_SAP.Service.AuthService
{
    public class AuthService(IMemoryCache memoryCache, IOptions<LoginRequest> option) : IAuthService
    {
        private readonly string LOGIN_TOKEN = "SAPToken";
        private string responseBody = string.Empty;

        public async Task<LoginResponse> LoginAsync()
        {
            try
            {
                if (memoryCache.TryGetValue(LOGIN_TOKEN, out LoginResponse? tokenSAP))
                {
                    if (tokenSAP is not null)
                        return tokenSAP;
                }

                LoginResponse? newResponse = new("", "", 0);

                HttpClientHandler clientHandler = new()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                var login = new LoginRequest(option.Value.UserName, option.Value.Password, option.Value.CompanyDB);

                string json = JsonSerializer.Serialize(login);

                StringContent content = new(json, Encoding.UTF8, "application/json");

                using (var client = new HttpClient(clientHandler))
                {
                    HttpResponseMessage response = await client.PostAsync("https://linux-7lxj:50000/b1s/v1/Login", content);

                    responseBody = await response.Content.ReadAsStringAsync();

                    newResponse = JsonSerializer.Deserialize<LoginResponse>(responseBody);
                }

                var memoryCacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1800),
                    SlidingExpiration = TimeSpan.FromSeconds(1500)
                };

                memoryCache.Set(LOGIN_TOKEN, newResponse, memoryCacheEntryOptions);

                return newResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                return null;
            }
        }

    }
}
