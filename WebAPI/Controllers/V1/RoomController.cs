using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers.V1
{
	public class RoomController : BaseApiController
	{
		private readonly IHotelDBContext _context;

		public RoomController(IHotelDBContext context)
		{
			_context = context;
		}
		[HttpGet("GetRoomInfo")]
		public async Task<ActionResult<IEnumerable<object>>> GetRoomInfo()
		{
			if (_context == null)
			{
				return StatusCode(500, "Internal Server Error: DbContext not injected");
			}

			var roomInfo = await _context.phong
				.Include(p => p.LoaiPhong)
				.Select(p => new
				{
					p.MaPhong,
					p.TrangThaiPhong,
					p.SoPhong,
					LoaiPhong = new
					{
						p.LoaiPhong.MaLoaiPhong,
						p.LoaiPhong.TenLoaiPhong,
						p.LoaiPhong.SoLuongPhong,
						p.LoaiPhong.Mota,
						p.LoaiPhong.Gia
					}
				})
				.ToListAsync();

			if (roomInfo == null || !roomInfo.Any())
			{
				return NotFound("Không tìm thấy dữ liệu phòng");
			}

			return roomInfo;

		}
	}
}
