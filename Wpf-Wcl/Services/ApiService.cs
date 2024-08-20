using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Wpf_Wcl.Common;
using Wpf_Wcl.Models;

namespace Wpf_Wcl.Services
{
    public class ApiService(IHttpClientFactory httpClientFactory) : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        public async Task<User> LoginAsync(NetworkCredential networkCredential)
        {
            var httpClient = _httpClientFactory.CreateClient("apiservice");
            var response = await httpClient.GetAsync($"user/login?username={networkCredential.UserName}&password={networkCredential.Password}");
            return response.IsSuccessStatusCode ? await GetUserAsync(networkCredential.UserName) : null;
        }

        public async Task<User> RegisterAsync(User user)
        {
            var httpClient = _httpClientFactory.CreateClient("apiservice");
            string jsonUser = JsonSerializer.Serialize(user, _jsonSerializerOptions);
            var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("user", content);
            return response.IsSuccessStatusCode ? await GetUserAsync(user.Username) : null;
        }

        public async Task LogoutAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("apiservice");
            _ = await httpClient.GetAsync("user/logout");
        }

        public async Task<User> GetUserAsync(string username)
        {
            var httpClient = _httpClientFactory.CreateClient("apiservice");
            var url = $"user/{username}";
            var response = await httpClient.GetAsync(url);
            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions)
                : null;
        }
    }
}
