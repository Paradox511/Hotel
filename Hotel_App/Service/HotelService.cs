
using Blazored.LocalStorage;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;

namespace Hotel_App.Service
{
    public class HotelService<T> : IHotelService<T>
    {
        private readonly HttpClient _httpClient;

        public HotelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> DeleteAsync(string requestUri, int Id)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri + Id);
            string url = requestUri + Id;
           
            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            return await Task.FromResult(true);
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

        public async Task<T> GetByIdAsync(string requestUri, int Id)
        {
            try
            {
                var response = await _httpClient.GetAsync(string.Format(requestUri, Id));
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<T>();
                return data;
            }
            catch (Exception ex)
            {
                // Handle exceptions here, e.g., log the error, return default value, or throw a custom exception
                Console.WriteLine("Error fetching data: " + ex.Message);
                return default(T);
            }
        
    }

        public async Task<T> SaveAsync(string requestUri, T obj)
        {
            string serializedUser = JsonConvert.SerializeObject(obj);
            var jsonWithRoot = "{\"entity\":" + serializedUser + "}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);

           

            requestMessage.Content = new StringContent(jsonWithRoot);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.EnsureSuccessStatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedObj = JsonConvert.DeserializeObject<T>(responseBody);

            return await Task.FromResult(returnedObj);
        }
        public async Task<T> UpdateAsync(string requestUri,int id, T obj)
        {
            string serializedUser = JsonConvert.SerializeObject(obj);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri + id);
          
         
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedObj = JsonConvert.DeserializeObject<T>(responseBody);

            return await Task.FromResult(returnedObj);
        }
    }
}
