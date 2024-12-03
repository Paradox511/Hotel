using Hotel_App.Data;
using System.Net.Http;

namespace Hotel_App.Srvice
{
	public class RoomService
	{
		HttpClient _httpClient;
		public RoomService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<IEnumerable<Phong>> GetRoomTypeIDAtRoomID(int Roomid)
		{
			try
			{
				var baseUrl = "https://localhost:44359/api"; // Replace with your actual base URL
				var url = $"{baseUrl}/Room/GetRoomTypeIDAtRoomID/{Roomid}";
				var response = await _httpClient.GetAsync(url);
				response.EnsureSuccessStatusCode();
				var data = await response.Content.ReadFromJsonAsync<IEnumerable<Phong>>();
				return data;
			}
			catch (Exception ex)
			{
				// Handle exceptions here, e.g., log the error, return an empty list, or throw a custom exception
				Console.WriteLine("Error fetching CTHoaDon data: " + ex.Message);
				return Enumerable.Empty<Phong>();
			}
		}
	}
}
