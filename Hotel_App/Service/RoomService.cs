﻿using Hotel_App.Data;
using System.Net.Http;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using System.Net;
using System.Text;

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

		public async Task<DatPhong> AddDatPhong(DatPhong datPhong)
		{
			try
			{
				HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:44359/api/Room/CreateBookingRoom", datPhong);

				if (response.IsSuccessStatusCode)
				{
					DatPhong newDatPhong = await response.Content.ReadFromJsonAsync<DatPhong>();
					return newDatPhong;
				}
				else
				{
					// Xử lý lỗi khi API trả về không thành công
					throw new HttpRequestException($"API trả về mã lỗi: {response.StatusCode}");
				}
			}
			catch (Exception ex)
			{
				// Xử lý lỗi khi gửi yêu cầu đến API
				throw new HttpRequestException($"Lỗi khi gửi yêu cầu đến API: {ex.Message}");
			}
		}

		public async Task<List<KhachHang>> GetCustomers()
		{
			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44359/api/Room/GetCustomers");

				if (response.IsSuccessStatusCode)
				{
					List<KhachHang> customers = await response.Content.ReadFromJsonAsync<List<KhachHang>>();
					return customers;
				}
				else
				{
					// Xử lý lỗi khi API trả về không thành công
					throw new HttpRequestException($"API trả về mã lỗi: {response.StatusCode}");
				}
			}
			catch (Exception ex)
			{
				// Xử lý lỗi khi gửi yêu cầu đến API
				throw new HttpRequestException($"Lỗi khi gửi yêu cầu đến API: {ex.Message}");
			}
		}

		public async Task<HoaDon> AddHoaDonAsync(HoaDon hoaDon)
		{
			var hoaDonJson = JsonConvert.SerializeObject(hoaDon);
			var content = new StringContent(hoaDonJson, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("CreateBillRoom", content);

			if (response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<HoaDon>(responseContent);
			}
			else
			{
				throw new HttpRequestException($"Lỗi khi thêm hóa đơn, mã lỗi: {response.StatusCode}");
			}
		}
        public async Task UpdateTotal(int id, decimal newTotal)
        {
            try
            {
                var baseUrl = "https://localhost:44359/api";
                var url = $"{baseUrl}/Room/update-total/{id}?newTotal={newTotal}";

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
