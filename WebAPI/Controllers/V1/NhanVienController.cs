using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class taikhoanController : BaseApiController
    {
        private readonly IHotelDBContext _context;
        private readonly IMediator _mediator;


        public taikhoanController(IHotelDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;


        }
        [HttpGet("Gettaikhoan")]
        public async Task<IActionResult> GetAll()
        {
            if (_context == null)
            {
                return StatusCode(500, "Internal Server Error: DbContext not injected");
            }

            var employees = await _context.taikhoan.ToListAsync(); 
            if (employees == null)
            {
                return NotFound("No employees found");
            }

            return Ok(employees);
        }

        [HttpGet("GetByID/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employees = await _context.taikhoan
                .FirstOrDefaultAsync(h => h.MaTaiKhoan == id);
            //.ThenInclude(ct => ct.dv)// Include related CTHoaDon entities
            //.FirstOrDefaultAsync(h => h.MaHoaDon == id);


            if (employees == null)
            {
                return NotFound("Employees not found");
            }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            return Ok(employees);
        }

        [HttpPost("Createtaikhoan")]
        public async Task<IActionResult> Createtaikhoan([FromBody] CreateCommand<TaiKhoan> command)
        {
            if (command == null || command.Entity == null)
            {
                return BadRequest("Invalid command data");
            }
            _context.taikhoan.Add(command.Entity);
            await _context.SaveChangesAsync();
            return Ok("Nhan Vien created successfully");
        }

        // Similar methods for updating and deleting KhachHang
        /// <summary>
        /// Deletes Product Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteTaiKhoan/{id}")]
        public async Task<IActionResult> Delete(int id) {
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
        [HttpPut("Updatetaikhoan/{id}")]
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
