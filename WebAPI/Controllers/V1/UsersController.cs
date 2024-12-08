using Application.Features.Commands;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IHotelDBContext _context;
        private readonly IMediator _mediator;
        private readonly JWTSettings _jwtSettings;

        public UsersController(IHotelDBContext context, IMediator mediator, IOptions<JWTSettings> jwtSettings)
        {
            _context = context;
            _mediator = mediator;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<TaiKhoan>>> GetUsers()
        {
            var users = await _context.taikhoan.ToListAsync();
            if (users == null || !users.Any())
            {
                return NotFound("No users found");
            }

            return Ok(users);
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<TaiKhoan>> GetUser(int id)
        {
            var user = await _context.taikhoan.FirstOrDefaultAsync(h => h.MaTaiKhoan == id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<TaiKhoan>> Login([FromBody] TaiKhoan taiKhoan)
        {
            if (taiKhoan == null || string.IsNullOrWhiteSpace(taiKhoan.Username) || string.IsNullOrWhiteSpace(taiKhoan.Password))
            {
                return BadRequest("Invalid login credentials.");
            }

            //var hashedPassword = Hotel_App.Utility.Encrypt(taiKhoan.Password);
            var user = await _context.taikhoan.FirstOrDefaultAsync(u => u.Username == taiKhoan.Username && u.Password == taiKhoan.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            await _context.SaveChangesAsync();


            return Ok(user);
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult<SignupRequest>> RegisterUser([FromBody] SignupRequest user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Invalid registration data.");
            }

            var existingUser = await _context.taikhoan.AnyAsync(u => u.Username == user.Username);
            if (existingUser)
            {
                return Conflict("Username already exists.");
            }

            var newUser = new TaiKhoan
            {
                Username = user.Username,
                Password = Hotel_App.Utility.Encrypt(user.Password)
            };

            _context.taikhoan.Add(newUser);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetUser), new { id = newUser.MaTaiKhoan }, user);
        }

        //private RefreshToken GenerateRefreshToken()
        //{
        //    var randomNumber = new byte[32];
        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(randomNumber);
        //        return new RefreshToken { Token = Convert.ToBase64String(randomNumber), Expiry = DateTime.UtcNow.AddDays(7) };
        //    }
        //}

        //private string GenerateAccessToken(int userId)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) }),
        //        Expires = DateTime.UtcNow.AddHours(1),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    }
}