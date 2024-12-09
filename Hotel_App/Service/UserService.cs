using Microsoft.Extensions.Options;
using Hotel_App.Data;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Hotel_App.Service
{
    public class UserService : IUserService
    {
        public readonly HttpClient _httpClient;
        public readonly AppSettings _appSettings; 

        public UserService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

            httpClient.BaseAddress = new Uri(_appSettings.DefaultConnection);

            _httpClient = httpClient;
        }

        public async Task<TaiKhoan> LoginAsync(TaiKhoan user)
        {
            var jsonContent = JsonConvert.SerializeObject(user);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44359/api/Users/Login", httpContent);
            var responsebody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TaiKhoan>(responseData);
            }

            // Xử lý lỗi nếu cần
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Login failed: {errorMessage}");
        }
        public async Task<TaiKhoan> RegisterUserAsync(TaiKhoan user)
        {
            var jsonContent = JsonConvert.SerializeObject(user);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44359/api/Users/RegisterUser", httpContent);
            var responsebody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TaiKhoan>(responseData);
            }

            // Xử lý lỗi nếu cần
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Registration failed: {errorMessage}");
        }
    }
}
