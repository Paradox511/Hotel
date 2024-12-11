using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class PTTTController : BaseApiController
    {
        private readonly IHotelDBContext _context;


        public PTTTController(IHotelDBContext context)
        {
            _context = context;


        }
        [HttpGet("GetPTTT")]
        public async Task<IActionResult> GetAll()
        {
            if (_context == null)
            {
                return StatusCode(500, "Internal Server Error: DbContext not injected");
            }

                var methods = await _context.ptthanhtoan.ToListAsync();
            if (methods == null)
            {
                return NotFound("No methods found");
            }

            return Ok(methods);
        }
        [HttpGet("GetByID/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employees = await _context.ptthanhtoan
                .FirstOrDefaultAsync(h => h.MaPhuongThuc == id);
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
