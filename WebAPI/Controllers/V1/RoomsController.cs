using Application.Features.Commands;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Hotel_App.Service;
using Microsoft.AspNetCore.Hosting.Server;
//using Hotel_App.Data;

namespace WebAPI.Controllers.V1
{
		public class RoomsController : BaseApiController
		{
			private readonly IHotelDBContext _context;
			
			//private readonly IRoomRepository _roomRepository;

			private readonly IMediator _mediator;
			private readonly ISender _sender;

			public RoomsController(IHotelDBContext context, IMediator mediator, ISender sender)
			{
				_context = context;
				_mediator = mediator;
				_sender = sender;
			}
			[HttpGet("GetRooms")]
			public async Task<IActionResult> GetAll()
			{
			if (_context == null)
			{
				return StatusCode(500, "Internal Server Error: DbContext not injected");
			}
			await AutoUpdateRoomsStatus();
			var rooms = await _context.phong.Where(Room => Room.TrangThai == 1).ToListAsync(); // Assuming your bills are stored in "hoadon" DbSet

			if (rooms == null)
			{
				return NotFound("No rooms found");
			}

			return Ok(rooms);
		}
		[HttpGet("/api/Rooms/{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var room = await _context.phong
				.Include(h => h.MaLoaiPhong)
				//.ThenInclude(ct => ct.)// Include related CTHoaDon entities
				.FirstOrDefaultAsync(h => h.MaPhong == id);

			if (room == null)
			{
				return NotFound("Room not found");
			}
			var options = new JsonSerializerOptions
			{
				ReferenceHandler = ReferenceHandler.IgnoreCycles
			};

			return Ok(JsonSerializer.Serialize(room, options));
		}

		
		[HttpPost("CreateRoom")]
			public async Task<IActionResult> CreateRoom([FromBody] CreateCommand<Phong> command)
			{
				if (command == null || command.Entity == null)
				{
					return BadRequest("Invalid command data");
				}
				
				_context.phong.Add(command.Entity);
				await _context.SaveChangesAsync();
				return Ok("Room created successfully");
			}

		

		[HttpPut("UpdateRoom/{id}")]
		public async Task<IActionResult> Update(int id, Phong rooms)
		{
			if (id != rooms.MaPhong)
			{
				return BadRequest("Invalid command data");
			}
			_context.phong.Entry(rooms).State = EntityState.Modified;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (rooms == null)
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return Ok(rooms); // No content to return on successful update
		}

		[HttpDelete("DeleteRoom/{id}")]
			public async Task<IActionResult> Delete(int id)
			{
			var room = await _context.phong.FindAsync(id);			
			if (room == null)
			{
				return BadRequest("Invalid command data");
			}

			// Cập nhật trạng thái của phòng từ 1 thành 0
			room.TrangThai = 0;
			try
			{
				await _context.SaveChangesAsync(); // Lưu thay đổi vào database
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Error updating room status.");
			}

			return Ok("Room status updated to 0");
		}

		[HttpDelete("AutoUpdateRoomsStatus")]
		public async Task<IActionResult> AutoUpdateRoomsStatus()
		{ 
			if (_context == null)
			{
				return StatusCode(500, "Internal Server Error: DbContext not injected");
			}
			var currentDateTime = DateTime.Now;
			string formatDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
			DateTime formattedDateTime = DateTime.Parse(formatDateTime);
			//lấy id của phòng đã được đặt
			//var Roombooked = await _context.datphong.Where(booking => booking.CheckOutDate > formattedDateTime).ToListAsync();
			var Roombooked = await _context.datphong.Where(booking => booking.CheckOutDate > formattedDateTime).ToListAsync();
			var roomidbooked = Roombooked.Select(booking => booking.MaPhong).ToList();
			//lay danh sach phong co trang thai 1 
			var rooms = await _context.phong.Where(Room => Room.TrangThai == 1).ToListAsync();

			// đổi trạng thái của tất cả phòng được đặt thành 0(đã đặt) nếu tồn tại trong Roombooked còn không thì set lại trạng thái thành 1(Trống)
				foreach (var room in rooms)
			{
				if (roomidbooked.Contains(room.MaPhong))
				{
					room.TrangThaiPhong = 0;
					_context.phong.Entry(room).State = EntityState.Modified;
				}
				else
				{
					room.TrangThaiPhong = 1;
					_context.phong.Entry(room).State = EntityState.Modified;
				}
			}
			await _context.SaveChangesAsync(); // Save all changes at once

			var phong = await _context.phong
				.Where(room => room.TrangThai == 1)
				.ToListAsync();

			return Ok("Room status updated to 0");
		}

	}
	
}
