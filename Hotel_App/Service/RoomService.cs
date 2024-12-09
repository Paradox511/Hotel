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
		public RoomService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		//private readonly IRoomRepository _phongRepository;
		//private readonly IGenericRepository<LoaiPhong> _loaiPhongRepository;
		//public RoomService(IRoomRepository phongRepository, IGenericRepository<LoaiPhong> loaiPhongRepository)
		//{
		//	_phongRepository = phongRepository;
		//	_loaiPhongRepository = loaiPhongRepository;
		//}
		//private readonly IRoomRepository _phongRepository;
		//private readonly IGenericRepository<LoaiPhong> _loaiPhongRepository;

		//public RoomService(IRoomRepository phongRepository, IGenericRepository<LoaiPhong> _loaiPhongRepository)
		//{
		//	_phongRepository = phongRepository;
		//	_loaiPhongRepository = loaiPhongRepository;
		//}

		//public async Task<IEnumerable<Phong>> GetAllPhongsAsync()
		//{
		//	return await _phongRepository.GetAllAsync();
		//}

		//public async Task<Phong> GetPhongByIdAsync(int id)
		//{
		//	return await _phongRepository.GetByIdAsync(id);
		//}

		//public async Task<IEnumerable<Phong>> GetPhongsByLoaiPhongIdAsync(int loaiPhongId)
		//{
		//	return await _phongRepository.GetPhongsByLoaiPhongIdAsync(loaiPhongId);
		//}

		//public async Task CreatePhongAsync(Phong phong)
		//{
		//	// Kiểm tra MaLoaiPhong có tồn tại không
		//	var loaiPhong = await _loaiPhongRepository.GetByIdAsync(phong.MaLoaiPhong);
		//	if (loaiPhong == null)
		//	{
		//		throw new ArgumentException("Loại phòng không tồn tại.");
		//	}

		//	await _phongRepository.CreateCommandAsync(phong);
		//}

		//public async Task UpdatePhongAsync(Phong phong)
		//{
		//	await _phongRepository.UpdateCommandAsync(phong);
		//}

		//public async Task DeletePhongAsync(int id)
		//{
		//	var phong = await _phongRepository.GetByIdAsync(id);
		//	if (phong != null)
		//	{
		//		await _phongRepository.DeleteCommandAsync(phong);
		//	}

		//}

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

		//public async Task<IEnumerable<Phong>> GetPhongsByLoaiPhongIdAsync(int loaiPhongId)
		//{
		//	return await _phongRepository.GetPhongsByLoaiPhongIdAsync(loaiPhongId);
		//}
	}
}
