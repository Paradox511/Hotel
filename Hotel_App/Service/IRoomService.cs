using Hotel_App.Data;
using Hotel_App.Service;

namespace Hotel_App.Service
{
	public interface IRoomService 
	{
		
		Task<IEnumerable<Phong>> GetPhongsByLoaiPhongIdAsync(int loaiPhongId);
		Task<Phong> DeletePhongAsync(Phong obj,int Id );
	}
}
