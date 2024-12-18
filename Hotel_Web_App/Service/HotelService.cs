﻿
namespace Hotel_Web_App.Service
{
    public class HotelService<T> : IHotelService<T>
    {
        private readonly HttpClient _httpClient;

        public HotelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<bool> DeleteAsync(string requestUri, int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string requestUri)
        {
            try
            {
                var response = await _httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
                return data;
            }
            catch (Exception ex)
            {
                // Handle exceptions here, e.g., log the error, return a default value, or throw a custom exception
                Console.WriteLine("Error fetching data: " + ex.Message);
                return Enumerable.Empty<T>(); // Or throw a custom exception
            }
        }

        public Task<T> GetByIdAsync(string requestUri, int Id)
        {
            throw new NotImplementedException();
        }

        public Task<T> SaveAsync(string requestUri, T obj)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(string requestUri, int Id, T obj)
        {
            throw new NotImplementedException();
        }
    }
}
