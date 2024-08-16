using System.Net.Http;
using Newtonsoft.Json;
using Wpf_Wcl.Common;
using Wpf_Wcl.Models;

namespace Wpf_Wcl.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://petstore.swagger.io/v2/") };
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var response = await _httpClient.PostAsync("user/login", new StringContent(JsonConvert.SerializeObject(new { username, password })));
            _ = response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
        }

        public async Task<User> RegisterAsync(User user)
        {
            var response = await _httpClient.PostAsync("user", new StringContent(JsonConvert.SerializeObject(user)));
            _ = response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
        }

        public async Task LogoutAsync()
        {
            var response = await _httpClient.GetAsync("user/logout");
            _ = response.EnsureSuccessStatusCode();
        }

        public async Task<User> GetUserAsync(string username)
        {
            var response = await _httpClient.GetAsync($"user/{username}");
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync()) : new();
        }
    }
}
