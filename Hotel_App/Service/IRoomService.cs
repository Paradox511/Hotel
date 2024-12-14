using Hotel_App.Data;
using Hotel_App.Service;

namespace Hotel_App.Service
{
	public interface IRoomService<T>
	{
		//public Task<List<T>> GetAllRoomTypes(string requestUri);
		//public Task<List<T>> GetAllRooms(string requestUri);
		
		Task<List<T>> GetRoomInfo();
	}
}
