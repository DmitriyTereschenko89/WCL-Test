using System.Net;
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

        public async Task<User> LoginAsync(NetworkCredential networkCredential)
        {
            var url = $"{_httpClient.BaseAddress}user/login?username={networkCredential.UserName}&password={networkCredential.Password}";
            var response = await _httpClient.GetAsync(url);
            return response.IsSuccessStatusCode
                ? await GetUserAsync(networkCredential.UserName)
                : JsonConvert.DeserializeObject<User>(string.Empty);
        }

        public async Task<User> RegisterAsync(User user)
        {
            /*curl -X 'POST' \
  'https://petstore.swagger.io/v2/user' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 0,
  "username": "admin",
  "firstName": "ivan",
  "lastName": "ivanov",
  "email": "test@gmail.com",
  "password": "12345",
  "phone": "111-11-11",
  "userStatus": 1
}'*/
            var response = await _httpClient.PostAsync("user", new StringContent(JsonConvert.SerializeObject(user)));
            return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
        }

        public async Task LogoutAsync()
        {
            _ = await _httpClient.GetAsync("user/logout");
        }

        public async Task<User> GetUserAsync(string username)
        {
            var url = $"{_httpClient.BaseAddress}user/{username}";
            var response = await _httpClient.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(data);
            return user;
        }
    }
}
