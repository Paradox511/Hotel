using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
