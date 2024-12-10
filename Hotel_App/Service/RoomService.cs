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

		public async Task<List<Phong>> GetAvailableRooms(string apiUrl, DateTime checkInDate, DateTime checkOutDate)
		{
			List<Phong> phongs = new List<Phong>();

			try
			{
				string formattedCheckInDate = checkInDate.ToString("yyyy-MM-dd");
				string formattedCheckOutDate = checkOutDate.ToString("yyyy-MM-dd");

				string url = $"{apiUrl}?checkInDate={formattedCheckInDate}&checkOutDate={formattedCheckOutDate}";

				HttpResponseMessage response = await _httpClient.GetAsync(url);
				var responsebody = response.Content.ReadAsStringAsync();

				response.EnsureSuccessStatusCode();

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

		//public async Task<int> GetAvailableRoomCountAsync(int maLoaiPhong, DateTime checkInDate, DateTime checkOutDate)
		//{
		//	string apiUrl = $"https://localhost:44359/api/Room/CountAvailableRooms?maLoaiPhong={maLoaiPhong}&checkInDate={checkInDate}&checkOutDate={checkOutDate}";

		//	try
		//	{
		//		HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

		//		if (response.IsSuccessStatusCode)
		//		{
		//			string responseContent = await response.Content.ReadAsStringAsync();
		//			int availableRoomCount = JsonSerializer.Deserialize<int>(responseContent, new JsonSerializerOptions
		//			{
		//				PropertyNameCaseInsensitive = true
		//			});
		//			return availableRoomCount;
		//		}
		//		else
		//		{
		//			return -1;
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		return -1;
		//	}
		//}
		public async Task<int> GetAvailableRoomCountAsync(int maLoaiPhong, DateTime checkInDate, DateTime checkOutDate)
		{
			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:44359/api/Room/CountAvailableRooms/{maLoaiPhong}?checkInDate={checkInDate}&checkOutDate={checkOutDate}");

				response.EnsureSuccessStatusCode(); // Throw exception if not successful

				int availableRoomCount = await response.Content.ReadFromJsonAsync<int>();
				return availableRoomCount;
			}
			catch (HttpRequestException ex)
			{
				// Log or handle the exception as needed
				throw new Exception("Error calling the API", ex);
			}
		}

		public async Task<Phong> GetByIdAsync(string requestUri, int Id, DateTime checkInDate, DateTime checkOutDate, decimal priceRoom, int typeRoomId)
		{
			try
			{
				var response = await _httpClient.GetAsync(string.Format(requestUri, Id));
				response.EnsureSuccessStatusCode();
				var data = await response.Content.ReadFromJsonAsync<Phong>();
				return data;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error fetching data: " + ex.Message);
				return default(Phong);
			}
		}

		public async Task<KhachHang> GetCustomerByIdAsync(string requestUri, int Id)
		{
			try
			{
				var response = await _httpClient.GetAsync(string.Format(requestUri, Id));
				response.EnsureSuccessStatusCode();
				var data = await response.Content.ReadFromJsonAsync<KhachHang>();
				return data;
			}
			catch (Exception ex)
			{
				// Handle exceptions here, e.g., log the error, return default value, or throw a custom exception
				Console.WriteLine("Error fetching data KHACH HANG: " + ex.Message);
				return default(KhachHang);
			}

		}

		public async Task<Phong> GetRoomByIdAsync(string requestUri, int Id)
		{
			try
			{
				var response = await _httpClient.GetAsync(string.Format(requestUri, Id));
				response.EnsureSuccessStatusCode();
				var data = await response.Content.ReadFromJsonAsync<Phong>();
				return data;
			}
			catch (Exception ex)
			{
				// Handle exceptions here, e.g., log the error, return default value, or throw a custom exception
				Console.WriteLine("Error fetching data KHACH HANG: " + ex.Message);
				return default(Phong);
			}
		}

		public async Task<LoaiPhong> GetRoomTypeByIdAsync(string requestUri, int Id)
		{
			try
			{
				var response = await _httpClient.GetAsync(string.Format(requestUri, Id));
				response.EnsureSuccessStatusCode();
				var data = await response.Content.ReadFromJsonAsync<LoaiPhong>();
				return data;
			}
			catch (Exception ex)
			{
				// Handle exceptions here, e.g., log the error, return default value, or throw a custom exception
				Console.WriteLine("Error fetching data KHACH HANG: " + ex.Message);
				return default(LoaiPhong);
			}
		}

	}
}
