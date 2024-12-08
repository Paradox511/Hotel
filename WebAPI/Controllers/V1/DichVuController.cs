using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class DichVuController : BaseApiController
    {
        private readonly IHotelDBContext _context;
        private readonly IMediator _mediator;
        private readonly ISender _sender;

        public DichVuController(IHotelDBContext context, IMediator mediator, ISender sender)
        {
            _context = context;
            _mediator = mediator;
            _sender = sender;

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            if (_context == null)
            {
                return StatusCode(500, "Internal Server Error: DbContext not injected");
            }

            var bills = await _context.dichvu.ToListAsync(); // Assuming your bills are stored in "hoadon" DbSet
            if (bills == null)
            {
                return NotFound("No bills found");
            }

            return Ok(bills);
        }
        [HttpPost("CreateDichVu")]
        public async Task<IActionResult> CreateDichVu([FromBody] CreateCommand<DichVu> command)
        {
            if (command == null || command.Entity == null)
            {
                return BadRequest("Invalid command data");
            }
            _context.dichvu.Add(command.Entity);
            await _context.SaveChangesAsync();
            return Ok("Bill created successfully");
        }

        // Similar methods for updating and deleting KhachHang
        /// <summary>
        /// Deletes Product Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteDichVu/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return BadRequest("Invalid command data");
            }
            var entity = _context.dichvu.Find(id);
            _context.dichvu.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(entity); // No content to return on successful deletion
        }
        /// <summary>
        /// Updates the Product Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("UpdateDichVu/{id}")]
        public async Task<IActionResult> Update(int id, DichVu dichVu)
        {
            if (id != dichVu.MaDichVu)
            {
                return BadRequest("Invalid command data");
            }
            _context.dichvu.Entry(dichVu).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (dichVu == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(dichVu); // No content to return on successful update
        }
    }
}
