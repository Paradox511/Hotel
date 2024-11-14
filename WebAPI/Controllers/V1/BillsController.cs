using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Queries;
using Application.Features.BillsFeatures.Queries;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class BillsController : BaseApiController
    {
        private readonly IHotelDBContext _context;

        public BillsController(IHotelDBContext context)
        {
            _context = context;
        }
        [HttpGet]
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
    }
}
