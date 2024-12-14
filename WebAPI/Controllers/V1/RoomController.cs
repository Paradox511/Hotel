using Application.Interfaces;
using Domain.Entities;
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

		[HttpPost("CreateBookingRoom")]
		public async Task<IActionResult> AddDatPhong(DatPhong datPhong)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_context.datphong.Add(datPhong);
					await _context.SaveChangesAsync();
					return Ok(datPhong); // Trả về đối tượng DatPhong đã được thêm thành công
				}
				return BadRequest(ModelState);
			}
			catch (DbUpdateException ex)
			{
				// Trích xuất thông báo lỗi từ inner exception
				var errorMessage = "Lỗi khi thêm dữ liệu vào bảng DatPhong: " + ex.InnerException?.Message;
				return StatusCode(500, errorMessage);
			}
		}

		[HttpPost("CreateBillRoom")]
		public async Task<IActionResult> AddHoaDon(HoaDon hoaDon)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_context.hoadon.Add(hoaDon);
					await _context.SaveChangesAsync();
					return Ok(hoaDon);
				}
				return BadRequest(ModelState);
			}
			catch (DbUpdateException ex)
			{
				// Trích xuất thông báo lỗi từ inner exception
				var errorMessage = "Lỗi khi thêm dữ liệu vào bảng DatPhong: " + ex.InnerException?.Message;
				return StatusCode(500, errorMessage);
			}
		}

		[HttpGet("GetCustomers")]
		public async Task<IActionResult> GetAllKhachHang()
		{
			if (_context == null)
			{
				return StatusCode(500, "Internal Server Error: DbContext not injected");
			}

			var customers = await _context.khachhang.Where(cus => cus.TrangThai == 1).ToListAsync();
			// Assuming your bills are stored in "hoadon" DbSet
			if (customers == null)
			{
				return NotFound("No Customers found");
			}

			return Ok(customers);
		}

        [HttpPut("update-total/{id}")]
        public async Task<IActionResult> UpdateTotal(int id, decimal newTotal)
        {
            // Retrieve the HoaDon entity from the database
            var hoaDon = await _context.datphong.FindAsync(id);

            if (hoaDon == null)
            {
                return NotFound();
            }

            // Update the TongSoTien property

            hoaDon.TongTien = newTotal;


            // Save the changes to the database
            _context.datphong.Entry(hoaDon).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(hoaDon);
        }
	}
}
