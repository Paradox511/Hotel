using Application.Features.Commands;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace WebAPI.Controllers.V1
{
		public class RoomsController : BaseApiController
		{
			private readonly IHotelDBContext _context;
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

				var rooms = await _context.phong.ToListAsync(); // Assuming your bills are stored in "hoadon" DbSet
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
				return Ok("Bill created successfully");
			}

			// Similar methods for updating and deleting KhachHang
			/// <summary>
			/// Deletes Product Entity based on Id.
			/// </summary>
			/// <param name="id"></param>
			/// <returns></returns>
			[HttpDelete("DeleteRoom")]
			public async Task<IActionResult> Delete([FromBody] DeleteCommand<Phong> command)
			{
				if (command == null || command.Id == null)
				{
					return BadRequest("Invalid command data");
				}
				var entity = _context.phong.Find(command.Id);
				_context.phong.Remove(entity);
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
			public async Task<IActionResult> Update([FromBody] UpdateCommand<HoaDon> command)
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
