using Hotel_App.Data;

namespace Hotel_App.Service
{
	public class RoomService
	{
		private readonly HttpClient _httpClient;

		public RoomService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<Phong>> GetRoomInfo(string apiUrl)
		{
			List<Phong> phongs = new List<Phong>();

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
				response.EnsureSuccessStatusCode(); // Đảm bảo rằng request không gặp lỗi

				if (response.IsSuccessStatusCode)
				{
					phongs = await response.Content.ReadFromJsonAsync<List<Phong>>();
				}
			}
			catch (Exception ex)
			{
				// Xử lý nếu có lỗi xảy ra
				Console.WriteLine("Lỗi khi lấy dữ liệu từ API: " + ex.Message);
			}

			return phongs;
		}

	}
}
