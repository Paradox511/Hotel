using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Queries;
using Application.Features.BillsFeatures.Queries;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.Features.Commands;
using System.Text.Json.Serialization;
using System.Text.Json;


namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class BillsController : BaseApiController
    {
        private readonly IHotelDBContext _context;
        private readonly IMediator _mediator;
        private readonly ISender _sender;

        public BillsController(IHotelDBContext context,IMediator mediator,ISender sender)
        {
            _context = context;
            _mediator = mediator;
            _sender = sender;

        }
        [HttpGet("GetBills")]
        public async Task<IActionResult> GetAll()
        {
            if (_context == null)
            {
                return StatusCode(500, "Internal Server Error: DbContext not injected");
            }

            var bills = await _context.hoadon.ToListAsync(); // Assuming your bills are stored in "hoadon" DbSet
            if (bills == null)
            {
                return NotFound("No bills found");
            }

            return Ok(bills);
        }
        [HttpGet("/api/Bills/details/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bill = await _context.hoadon
                .Include(h => h.CTHoaDon)
                .ThenInclude(ct => ct.dv)// Include related CTHoaDon entities
                .FirstOrDefaultAsync(h => h.MaHoaDon == id);

            if (bill == null)
            {
                return NotFound("Bill not found");
            }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            return Ok(JsonSerializer.Serialize(bill, options));
        }


        [HttpPost("CreateBill")]
        public async Task<IActionResult> CreateBill([FromBody] CreateCommand<HoaDon> command)
        {
            if (command == null || command.Entity == null)
            {
                return BadRequest("Invalid command data");
            }
            _context.hoadon.Add(command.Entity);
            await _context.SaveChangesAsync();
            return Ok("Bill created successfully");
        }

        // Similar methods for updating and deleting KhachHang
        /// <summary>
        /// Deletes Product Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCommand<HoaDon> command)
        {
            if (command == null || command.Id == null)
            {
                return BadRequest("Invalid command data");
            }
            var entity = _context.hoadon.Find(command.Id);
            _context.hoadon.Remove(entity);
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
