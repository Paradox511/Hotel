﻿using Application.Features.Commands;
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

            var employees = await _context.nhanvien.ToListAsync(); 
            if (employees == null)
            {
                return NotFound("No employees found");
            }

            return Ok(employees);
        }



        //[HttpGet("GetByIDorName")]
        //public async Task<IActionResult> GetById(int id, string name)
        //{
        //    var employees = await _context.nhanvien
        //        .Include(h => h.MaNhanVien == id || h.HoTen == name);
        //        //.ThenInclude(ct => ct.dv)// Include related CTHoaDon entities
        //        //.FirstOrDefaultAsync(h => h.MaHoaDon == id);

        //    if (employees == null)
        //    {
        //        return NotFound("Employees not found");
        //    }
        //    var options = new JsonSerializerOptions
        //    {
        //        ReferenceHandler = ReferenceHandler.IgnoreCycles
        //    };

        //    return Ok(JsonSerializer.Serialize(employees, options));
        //}

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
        [HttpDelete("DeleteNhanVien/{id}")]
        public async Task<IActionResult> Delete(int id) { 
            if (id == null)
            {
                return BadRequest("Invalid command data");
            }
            var entity = _context.nhanvien.Find(id);
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
        [HttpPut("UpdateNhanVien/{id}")]
        public async Task<IActionResult> Update(int id, NhanVien employee)
        {
            if (id != employee.MaNhanVien)
            {
                return BadRequest("Invalid command data");
            }
            _context.nhanvien.Entry(employee).State = EntityState.Modified;
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
