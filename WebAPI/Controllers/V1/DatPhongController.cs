using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class DatPhongController : BaseApiController
    {
        private readonly IHotelDBContext _context;


        public DatPhongController(IHotelDBContext context)
        {
            _context = context;


        }
        [HttpGet("GetDatPhong")]
        public async Task<IActionResult> GetAll()
        {
            if (_context == null)
            {
                return StatusCode(500, "Internal Server Error: DbContext not injected");
            }

            var reservations = await _context.datphong.ToListAsync(); // Assuming your bills are stored in "hoadon" DbSet
            if (reservations == null)
            {
                return NotFound("No bills found");
            }

            return Ok(reservations);
        }

        [HttpGet("GetByID/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employees = await _context.datphong
                .FirstOrDefaultAsync(h => h.MaDatPhong == id);
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

            return Ok(JsonSerializer.Serialize(employees, options));
        }
    }
}
