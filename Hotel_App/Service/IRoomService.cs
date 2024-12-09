using Hotel_App.Data;

namespace Hotel_App.Service
{
	public interface IRoomService
	{
		Task<IEnumerable<Phong>> GetPhongsByLoaiPhongIdAsync(int loaiPhongId);
		
		}
}
