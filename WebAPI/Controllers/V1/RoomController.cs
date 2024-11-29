using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers.V1
{
	public class RoomController : BaseApiController
	{
		private readonly IHotelDBContext _context;
		private readonly IMediator _mediator;
		private readonly ISender _sender;

		public RoomController(IHotelDBContext context)
		{
			_context = context;
		}
		[HttpGet("GetRoomInfo")]
		public async Task<IActionResult> GetRoomInfo()
		{
			if (_context == null)
			{
				return StatusCode(500, "Internal Server Error: DbContext not injected");
			}
			var loaiPhong = await _context.loaiphong.ToListAsync();
			var phong = await _context.phong.Include(p => p.LoaiPhong).ToListAsync();

			if (phong == null || loaiPhong == null)
			{
				return NotFound("No Phong types or LoaiPhong found");
			}
			return Ok(new { Phong = phong, LoaiPhong = loaiPhong });
		}
	}
}
