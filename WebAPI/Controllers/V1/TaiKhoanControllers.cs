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
            var customers = await _context.taikhoan.Where(cus => cus.TrangThai == 1).ToListAsync();
            // Assuming your bills are stored in "hoadon" DbSet
            if (customers == null)
            {
                return NotFound("No Accounts found");
            }

            return Ok(customers);
        }


        [HttpPost("CreateTaiKhoan")]
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



        [HttpDelete("DeleteTaiKhoan/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.taikhoan.FindAsync(id);
            if (employee == null)
            {
                return BadRequest("Invalid command data");
            }
            employee.TrangThai = 0;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: DbContext not injected");
            }
            return Ok("employee status updated to 0");
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

        [HttpPut("UpdateTaiKhoan/{id}")]
        public async Task<IActionResult> Update(int id, TaiKhoan employee)
        {
            if (id != employee.MaTaiKhoan)
            {
                return BadRequest("Invalid command data");
            }
            _context.taikhoan.Entry(employee).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (employee == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(employee); // No content to return on successful update
        }



    }
}
