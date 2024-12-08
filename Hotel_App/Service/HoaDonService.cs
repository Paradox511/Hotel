using Hotel_App.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Hotel_App.Service
{
    public class HoaDonService : IHoaDonService
    {
        HttpClient _httpClient;
        public HoaDonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CTHoaDon>> GetCTHoaDonByMaHoaDon(int id)
        {
            try
            {
                var baseUrl = "https://localhost:44359/api"; // Replace with your actual base URL
                var url = $"{baseUrl}/Bills/details/{id}";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<IEnumerable<CTHoaDon>>();
                return data;
            }
            catch (Exception ex)
            {
                // Handle exceptions here, e.g., log the error, return an empty list, or throw a custom exception
                Console.WriteLine("Error fetching CTHoaDon data: " + ex.Message);
                return Enumerable.Empty<CTHoaDon>();
            }
        }
        public async Task UpdateTotal(int id,decimal newTotal)
        {
            try
            {
                var baseUrl = "https://localhost:44359/api";
                var url = $"{baseUrl}/Bills/update-total/{id}?newTotal={newTotal}";

                // No need to create an object and serialize it, as the query parameter will be used directly in the URL

                var response = await _httpClient.PutAsync(url, null); // No content needed for this PUT request
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine("Error updating total: " + ex.Message);
            }
        }
        public async Task UpdateQuantity(int id, int newQuantity)
        {
            try
            {
                var baseUrl = "https://localhost:44359/api";
                var url = $"{baseUrl}/BillDetails/update-quantity/{id}?newQuantity={newQuantity}";

                // No need to create an object and serialize it, as the query parameter will be used directly in the URL

                var response = await _httpClient.PutAsync(url, null); // No content needed for this PUT request
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine("Error updating total: " + ex.Message);
            }
        }
    
}
}
