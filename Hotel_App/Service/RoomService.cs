using Hotel_App.Data;
using System.Net.Http;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Hotel_App.Service
{
	public class RoomService : IRoomService
	{
		//private readonly IRoomRepository _phongRepository;
		HttpClient _httpClient;
		string requestUri= "https:localhost:44359/api/Rooms/DeleteRoom/";
		public RoomService(HttpClient httpClient)
		{
			
			_httpClient = httpClient;
		}

		public async Task<IEnumerable<Phong>> GetPhongsByLoaiPhongIdAsync(int loaiPhongId)
		{
			try
			{
				var baseUrl = "https://localhost:44359/api"; // Replace with your actual base URL
				var url = $"{baseUrl}/RoomTypes/details/{loaiPhongId}";

				var response = await _httpClient.GetAsync(url);
				response.EnsureSuccessStatusCode();
				var data = await response.Content.ReadFromJsonAsync<IEnumerable<Phong>>();
				return data;
			}
			catch (Exception ex)
			{
				// Handle exceptions here, e.g., log the error, return an empty list, or throw a custom exception
				Console.WriteLine("Error fetching Phong data: " + ex.Message);
				return Enumerable.Empty<Phong>();
			}
		}

		public async Task<Phong> DeletePhongAsync(Phong obj,int id)
		{
			string serializedUser = JsonConvert.SerializeObject(obj);
			var baseUrl = "https://localhost:44359/api"; // Replace with your actual base URL
			var url = $"{baseUrl}/Rooms/DeleteRoom/{id}";

			var requestMessage = new HttpRequestMessage(HttpMethod.Put, url);


			requestMessage.Content = new StringContent(serializedUser);

			requestMessage.Content.Headers.ContentType
				= new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

			var response = await _httpClient.SendAsync(requestMessage);

			var responseStatusCode = response.StatusCode;
			var responseBody = await response.Content.ReadAsStringAsync();

			var returnedObj = JsonConvert.DeserializeObject<Phong>(responseBody);

			return await Task.FromResult(returnedObj);

			//var jsonContent = JsonConvert.SerializeObject(obj);
			//var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			//var response = await _httpClient.PostAsync($"https://localhost:44359/api/Rooms/DeleteRoom/{id}", httpContent);
			//var responsebody = await response.Content.ReadAsStringAsync();
			//if (response.IsSuccessStatusCode)
			//{
			//	var responseData = await response.Content.ReadAsStringAsync();
			//	return JsonConvert.DeserializeObject<Phong>(responseData);
			//}

			//// Xử lý lỗi nếu cần
			//var errorMessage = await response.Content.ReadAsStringAsync();
			//throw new Exception($"Room failed: {errorMessage}*");

		}

	}
}
