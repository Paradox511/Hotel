namespace Hotel_App.Service
{
	public class RoomService<T> : IRoomService<T>
	{
		private readonly HttpClient _httpClient;
		public RoomService(HttpClient _httpClient)
		{
			_httpClient = _httpClient;
		}

		public async Task<List<T>> GetAllRooms(string requestUri)
		{
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<T>>(requestUri);
				return response;
			}
			catch (Exception ex)
			{
				// Handle exception
				throw new Exception("Error retrieving rooms: " + ex.Message);
			}
		}

		public async Task<List<T>> GetAllRoomTypes(string requestUri)
		{
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<T>>(requestUri);
				return response;
			}
			catch (Exception ex)
			{
				// Handle exception
				throw new Exception("Error retrieving room types: " + ex.Message);
			}
		}
	}
}
