using Hotel_App.Data;
namespace Hotel_App.Service
{
	public class RoomTypeService
	{
		HttpClient _httpClient;
		public RoomTypeService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}		
	}
}

