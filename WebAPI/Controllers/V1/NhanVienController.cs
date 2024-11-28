using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class NhanVienController : BaseApiController
    {
        private readonly IHotelDBContext _context;
        private readonly IMediator _mediator;


        public NhanVienController(IHotelDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;


        }
        [HttpGet("GetNhanVien")]
        public async Task<IActionResult> GetAll()
        {
            if (_context == null)
            {
                return StatusCode(500, "Internal Server Error: DbContext not injected");
            }

            var employees = await _context.nhanvien.ToListAsync(); // Assuming your bills are stored in "hoadon" DbSet
            if (employees == null)
            {
                return NotFound("No bills found");
            }

            return Ok(employees);
        }

        [HttpPost("CreateNhanVien")]
        public async Task<IActionResult> CreateNhanVien([FromBody] CreateCommand<NhanVien> command)
        {
            if (command == null || command.Entity == null)
            {
                return BadRequest("Invalid command data");
            }
            _context.nhanvien.Add(command.Entity);
            await _context.SaveChangesAsync();
            return Ok("Nhan Vien created successfully");
        }

        // Similar methods for updating and deleting KhachHang
        /// <summary>
        /// Deletes Product Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteNhanVien")]
        public async Task<IActionResult> Delete([FromBody] DeleteCommand<NhanVien> command)
        {
            if (command == null || command.Id == null)
            {
                return BadRequest("Invalid command data");
            }
            var entity = _context.nhanvien.Find(command.Id);
            _context.nhanvien.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok("Nhan vien deleted."); // No content to return on successful deletion
        }
        /// <summary>
        /// Updates the Product Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateCommand<NhanVien> command)
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
