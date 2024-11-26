using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class NhanVienController : BaseApiController
    {
        private readonly IHotelDBContext _context;


        public NhanVienController(IHotelDBContext context)
        {
            _context = context;


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
     
    }
}
