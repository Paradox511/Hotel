using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Hotel_App.Pages.Manager;
using Application.Features.Commands;
using Domain.Entities;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class BillDetailsController : BaseApiController
    {
        private readonly IHotelDBContext _context;

        public BillDetailsController(IHotelDBContext context)
        {
            _context = context;
        }
        [HttpGet("/api/BillDetails/details/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bill = await _context.cthoadon
                .Include(h => h.dv)
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
        [HttpPut("update-quantity/{id}")]
        public async Task<IActionResult> UpdateTotal(int id, int newQuantity)
        {
            // Retrieve the HoaDon entity from the database
            var hoaDon = await _context.cthoadon
                .Include(h => h.dv)
                .FirstOrDefaultAsync(h => h.Macthd == id);

            if (hoaDon == null)
            {
                return NotFound();
            }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            // Update the TongSoTien property

            hoaDon.SoLuong = newQuantity;
            hoaDon.TongTien = hoaDon.dv.Gia * newQuantity;


            // Save the changes to the database
            _context.cthoadon.Entry(hoaDon).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(JsonSerializer.Serialize(hoaDon, options));
        }
        [HttpDelete("DeleteBillDetail/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return BadRequest("Invalid command data");
            }
            var entity = _context.cthoadon.Find(id);
            _context.cthoadon.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(entity); // No content to return on successful deletion
        }
        [HttpPost("CreateBillDetail")]
        public async Task<IActionResult> CreateBill([FromBody] CreateCommand<CTHoaDon> command)
        {
            if (command == null || command.Entity == null)
            {
                return BadRequest("Invalid command data");
            }
            _context.cthoadon.Add(command.Entity);
            await _context.SaveChangesAsync();
            return Ok("Bill created successfully");
        }
    }
}
