using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace WebAPI.Controllers.V1
{
	[ApiVersion("1.0")]
	public class BillsController : BaseApiController
	{
		private readonly IHotelDBContext _context;
		private readonly IMediator _mediator;
		private readonly ISender _sender;

		public BillsController(IHotelDBContext context, IMediator mediator, ISender sender)
		{
			_context = context;
			_mediator = mediator;
			_sender = sender;

        }
        [HttpGet("GetBills")]
        public async Task<IActionResult> GetAll()
        {
          
            if (_context == null)
            {
                return StatusCode(500, "Internal Server Error: DbContext not injected");
            }

            var bills = await _context.hoadon
                .Where(hd => hd.TrangThai == 1)
                .ToListAsync(); // Assuming your bills are stored in "hoadon" DbSet
            if (bills == null)
            {
                return NotFound("No bills found");
            }

			return Ok(bills);
		}
		[HttpGet("/api/Bills/details/{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var bill = await _context.hoadon
				.Include(h => h.CTHoaDon)
				.ThenInclude(ct => ct.dv)// Include related CTHoaDon entities
				.FirstOrDefaultAsync(h => h.MaHoaDon == id);

			if (bill == null)
			{
				return NotFound("Bill not found");
			}
			var options = new JsonSerializerOptions
			{
				ReferenceHandler = ReferenceHandler.IgnoreCycles
			};

			return Ok(JsonSerializer.Serialize(bill, options));
		}


		[HttpPost("CreateBill")]
		public async Task<IActionResult> CreateBill([FromBody] CreateCommand<HoaDon> command)
		{
			if (command == null || command.Entity == null)
			{
				return BadRequest("Invalid command data");
			}
			_context.hoadon.Add(command.Entity);
			await _context.SaveChangesAsync();
			return Ok("Bill created successfully");
		}

        // Similar methods for updating and deleting KhachHang
        /// <summary>
        /// Deletes Product Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("UpdateBillStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            if (id == null)
            {
                return BadRequest("Invalid command data");
            }

            var entity = await _context.hoadon.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.TrangThai = 0; // Set the TrangThai to 0
            await _context.SaveChangesAsync();

            return Ok(entity);
        }
        /// <summary>
        /// Updates the Product Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("UpdateBill/{id}")]
        public async Task<IActionResult> Update(int id , HoaDon bill)
        {
            if (id != bill.MaHoaDon)
            {
                return BadRequest("Invalid command data");
            }
            _context.hoadon.Entry(bill).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (bill==null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(bill); // No content to return on successful update
        }
        [HttpPut("DisableBill/{id}")]
        public async Task<IActionResult> Disable(int id, HoaDon bill)
        {
            if (id != bill.MaHoaDon)
            {
                return BadRequest("Invalid command data");
            }
            _context.hoadon.Entry(bill).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (bill == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(bill); // No content to return on successful update
        }

        // ... other actions ...

        [HttpPut("update-total/{id}")]
            public async Task<IActionResult> UpdateTotal(int id, decimal newTotal)
            {
                // Retrieve the HoaDon entity from the database
                var hoaDon = await _context.hoadon.FindAsync(id);

                if (hoaDon == null)
                {
                    return NotFound();
                }

                // Update the TongSoTien property
               
            hoaDon.TongSoTien = newTotal;


            // Save the changes to the database
            _context.hoadon.Entry(hoaDon).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(hoaDon);
            }
        

    }
}
