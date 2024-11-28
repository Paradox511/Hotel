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

			var roomtypes = await _context.loaiphong.ToListAsync(); // Assuming your bills are stored in "hoadon" DbSet
			if (roomtypes == null)
			{
				return NotFound("No rooms found");
			}

			return Ok(roomtypes);
		}
		//[HttpGet("/api/RoomTypes/{id}")]
		//public async Task<IActionResult> GetById(int id)
		//{
		//	var room = await _context.loaiphong
		//		.Include(h => h.MaLoaiPhong)
		//		//.ThenInclude(ct => ct.)// Include related CTHoaDon entities
		//		.FirstOrDefaultAsync(h => h.MaLoaiPhong == id);

		//	if (room == null)
		//	{
		//		return NotFound("Room not found");
		//	}
		//	var options = new JsonSerializerOptions
		//	{
		//		ReferenceHandler = ReferenceHandler.IgnoreCycles
		//	};

		//	return Ok(JsonSerializer.Serialize(room, options));
		//}


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

		// Similar methods for updating and deleting KhachHang
		/// <summary>
		/// Deletes Product Entity based on Id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("DeleteRoomStyle")]
		public async Task<IActionResult> Delete([FromBody] DeleteCommand<LoaiPhong> command)
		{
			if (command == null || command.Id == null)
			{
				return BadRequest("Invalid command data");
			}
			var entity = _context.loaiphong.Find(command.Id);
			_context.loaiphong.Remove(entity);
			await _context.SaveChangesAsync();
			return Ok("Bill deleted."); // No content to return on successful deletion
		}
		/// <summary>
		/// Updates the Product Entity based on Id.   
		/// </summary>
		/// <param name="id"></param>
		/// <param name="command"></param>
		/// <returns></returns>
		[HttpPut("[action]")]
		public async Task<IActionResult> Update([FromBody] UpdateCommand<LoaiPhong> command)
		{
			if (command == null || command.Entity == null)
			{
				return BadRequest("Invalid command data");
			}

			await _mediator.Send(command);
			return NoContent(); // No content to return on successful update
		}

	}
}
