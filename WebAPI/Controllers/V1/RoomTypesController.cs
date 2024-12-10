using Application.Features.Commands;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers.V1
{
	public class RoomTypesController : BaseApiController
	{
		private readonly IHotelDBContext _context;
		private readonly IMediator _mediator;
		private readonly ISender _sender;

		public RoomTypesController(IHotelDBContext context, IMediator mediator, ISender sender)
		{
			_context = context;
			_mediator = mediator;
			_sender = sender;
		}
		[HttpGet("GetRoomTypes")]
		public async Task<IActionResult> GetAll()
		{
			if (_context == null)
			{
				return StatusCode(500, "Internal Server Error: DbContext not injected");
			}

			var roomtypes = await _context.loaiphong.Where(RoomType=>RoomType.TrangThai==1).ToListAsync(); // Assuming your bills are stored in "hoadon" DbSet
			if (roomtypes == null)
			{
				return NotFound("No rooms found");
			}

			return Ok(roomtypes);
		}

		[HttpGet("/api/RoomTypes/GetRoomType/{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var loaiphong = await _context.loaiphong
				.Where(h => h.MaLoaiPhong == id)
				//.ThenInclude(ct => ct.)// Include related CTHoaDon entities
				//.FirstOrDefaultAsync(h => h.MaLoaiPhong == id);
				.FirstOrDefaultAsync();

			if (loaiphong == null)
			{
				return NotFound("Room not found");
			}
			var options = new JsonSerializerOptions
			{
				ReferenceHandler = ReferenceHandler.IgnoreCycles
			};

			return Ok(JsonSerializer.Serialize(loaiphong, options));
		}

		[HttpPost("CreateRoomType")]
		public async Task<IActionResult> CreateRoom([FromBody] CreateCommand<LoaiPhong> command)
		{
			if (command == null || command.Entity == null)
			{
				return BadRequest("Invalid command data");
			}
			_context.loaiphong.Add(command.Entity);
			await _context.SaveChangesAsync();
			return Ok("Roomtypes created successfully");
		}

		[HttpPut("UpdateRoomType/{id}")]
		public async Task<IActionResult> Update(int id, LoaiPhong typer)
		{
			if (id != typer.MaLoaiPhong)
			{
				return BadRequest("Invalid command data");
			}
			_context.loaiphong.Entry(typer).State = EntityState.Modified;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (typer == null)
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return Ok(typer); // No content to return on successful update
		}

		[HttpDelete("DeleteRoomType/{id}")]
		public async Task<IActionResult> Delete(int id)
		{		
			var roomtype = await _context.loaiphong.FindAsync(id);
			if (roomtype == null)
			{
				return BadRequest("Invalid command data");
			}
			roomtype.TrangThai = 0;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal Server Error: DbContext not injected");
			}
			return Ok("RoomType status updated to 0");
		}
	}
}
