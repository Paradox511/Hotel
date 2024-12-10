using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

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
		public async Task<ActionResult<IEnumerable<object>>> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
		{
			if (_context == null)
			{
				return StatusCode(500, "Internal Server Error: DbContext not injected");
			}

			var bookedRooms = await _context.datphong
				.Where(d => (checkInDate < d.CheckOutDate && checkOutDate > d.CheckInDate))
				.Select(d => d.MaPhong)
				.ToListAsync();

			var availableRooms = await _context.phong
				.Include(p => p.LoaiPhong)
				.Where(p => p.TrangThaiPhong == 1 && !bookedRooms.Contains(p.MaPhong))
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

			if (availableRooms == null || !availableRooms.Any())
			{
				return NotFound("Không có phòng trống trong khoảng thời gian đã chọn");
			}

			return availableRooms;
		}

		//[HttpGet("CountAvailableRooms")]
		//public async Task<int> CountAvailableRooms(int maLoaiPhong, DateTime checkInDate, DateTime checkOutDate)
		//{
		//	// Lấy danh sách phòng đã được đặt trong khoảng thời gian checkInDate và checkOutDate
		//	var phongsDaDat = await _context.datphong
		//		.Where(d => d.CheckInDate <= checkOutDate && d.CheckOutDate >= checkInDate)
		//		.Select(d => d.MaPhong)
		//		.ToListAsync();

		//	// Đếm số lượng loại phòng thỏa mãn điều kiện
		//	var count = await _context.phong
		//		.Where(p => p.TrangThaiPhong == 1 && p.MaLoaiPhong == maLoaiPhong && !phongsDaDat.Contains(p.MaPhong))
		//		.CountAsync();

		//	return count;
		//}

		[HttpGet("CountAvailableRooms/{maLoaiPhong}")]
		public IActionResult CountAvailableRooms(int maLoaiPhong, DateTime checkInDate, DateTime checkOutDate)
		{
			try
			{
				int availableRoomCount = _context.phong.Count(p => p.MaLoaiPhong == maLoaiPhong && p.TrangThaiPhong == 1 && !_context.datphong.Any(dp => dp.MaPhong == p.MaPhong && dp.CheckInDate <= checkOutDate && dp.CheckOutDate >= checkInDate));

				return Ok(availableRoomCount);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Đã xảy ra lỗi trong quá trình xử lý.");
			}
		}

		[HttpGet("/api/Room/GetRoomDetails/{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var details = await _context.phong
				.Include(h => h.LoaiPhong)
				.Where(h => h.MaPhong == id)
				.Select(h => new
				{
					h.MaPhong,
					h.TrangThaiPhong,
					h.SoPhong,
					LoaiPhong = new
					{
						h.LoaiPhong.MaLoaiPhong,
						h.LoaiPhong.TenLoaiPhong,
						h.LoaiPhong.SoLuongPhong,
						h.LoaiPhong.Mota,
						h.LoaiPhong.Gia
					}
				})
				.FirstOrDefaultAsync();

			if (details == null)
			{
				return NotFound("details not found");
			}

			return Ok(details);
		}

		[HttpGet("api/Room/GetCustomerInfo/{id}")]
		public async Task<IActionResult> GetKhachHangById(int id)
		{
			var khachHang = await _context.khachhang.FindAsync(id);

			if (khachHang == null)
			{
				return NotFound("KHACH HANG not found");
			}

			var options = new JsonSerializerOptions
			{
				ReferenceHandler = ReferenceHandler.IgnoreCycles
			};

			return Ok(JsonSerializer.Serialize(khachHang, options));
		}
		[HttpGet("GetRoomInfo/{id}")]
		public async Task<IActionResult> GetRoomById(int id)
		{
			var phong = await _context.phong.FindAsync(id);

			if (phong == null)
			{
				return NotFound("KHACH HANG not found");
			}

			var options = new JsonSerializerOptions
			{
				ReferenceHandler = ReferenceHandler.IgnoreCycles
			};

			return Ok(JsonSerializer.Serialize(phong, options));
		}

		[HttpGet("GetRoomTypeInfo/{id}")]
		public async Task<IActionResult> GetRoomTypeById(int id)
		{
			var loaiPhong = await _context.loaiphong.FindAsync(id);

			if (loaiPhong == null)
			{
				return NotFound("KHACH HANG not found");
			}

			var options = new JsonSerializerOptions
			{
				ReferenceHandler = ReferenceHandler.IgnoreCycles
			};

			return Ok(JsonSerializer.Serialize(loaiPhong, options));
		}
	}
}
