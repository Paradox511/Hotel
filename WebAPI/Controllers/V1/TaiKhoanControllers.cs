using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class TaiKhoanController : BaseApiController
    {
        private readonly IHotelDBContext _context;
        private readonly IMediator _mediator;
        private readonly ISender _sender;

        public TaiKhoanController(IHotelDBContext context, IMediator mediator, ISender sender)
        {
            _context = context;
            _mediator = mediator;
            _sender = sender;

        }
        [HttpGet("GetTaiKhoan")]
        public async Task<IActionResult> GetAll()
        {
            if (_context == null)
            {
                return StatusCode(500, "Internal Server Error: DbContext not injected");
            }

            var customers = await _context.taikhoan.ToListAsync(); // Assuming your bills are stored in "hoadon" DbSet
            if (customers == null)
            {
                return NotFound("No Accounts found");
            }

            return Ok(customers);
        }


        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateCommand<TaiKhoan> command)
        {
            if (command == null || command.Entity == null)
            {
                return BadRequest("Invalid command data");
            }
            _context.taikhoan.Add(command.Entity);
            await _context.SaveChangesAsync();
            return Ok("Account created successfully");
        }



        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return BadRequest("Invalid command data");
            }
            var entity = _context.taikhoan.Find(id);
            _context.taikhoan.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(entity); // No content to return on successful deletion
        }
        /// <summary>
        /// Updates the Product Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("UpdateCustomer/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateCommand<TaiKhoan> command)
        {
            if (command == null || command.Entity == null)
            {
                return BadRequest("Invalid command data");
            }
            _context.taikhoan.Entry(command.Entity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (command.Entity == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent(); // No content to return on successful update
        }

    }
}
