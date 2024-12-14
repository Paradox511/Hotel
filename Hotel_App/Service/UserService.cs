using Microsoft.Extensions.Options;
using Hotel_App.Data;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public async Task<TaiKhoan> LoginAsync(String users, String pass)
        {
           // var jsonContent = JsonConvert.SerializeObject(user);
            //var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"https://localhost:44359/api/Users/Login?users={users}&pass={pass}",null);
            var responsebody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                //var id = await response.Content.Where<TaiKhoan>(u=>u.responseData.MaTaiKhoan==u.id);
                //var id = await responsebody.Where(u => u.tai.MaTaiKhoan == u.id).FirstOrDefaultAsync();
                return JsonConvert.DeserializeObject<TaiKhoan>(responseData);
            }

            // Xử lý lỗi nếu cần
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Login failed: {errorMessage}*");
        }
        public async Task<TaiKhoan> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44359/api/Users/Login?id={id}");
            var responseBody = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<TaiKhoan>();
            return data;
        }
    }
}
