using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using JWTNotesAPI.Data;
using JWTNotesAPI.DTOs;
using JWTNotesAPI.Models;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTNotesAPI.Controllers
{
    [ApiController]

    [Route("api/auth")]

    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IConfiguration _configuration;


        public AuthController(
            AppDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // REGISTER API
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            // Check existing username
            if (await _context.Users
                .AnyAsync(u => u.Username == dto.Username))
            {
                return BadRequest(new
                {
                    message = "Username already exists."
                });
            }


            // Hash password
            var hashedPassword =
                BCrypt.Net.BCrypt.HashPassword(dto.Password);


            // Create user object
            var user = new User
            {
                Username = dto.Username,
                PasswordHash = hashedPassword
            };


            // Save user
            _context.Users.Add(user);

            await _context.SaveChangesAsync();


            return Ok(new
            {
                message =
                    "User registered successfully. Please log in."
            });
        }


        // LOGIN API
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            // Find user
            var user = await _context.Users
                .FirstOrDefaultAsync(
                    u => u.Username == dto.Username);

            // Invalid username
            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password."
                });
            }


            // Verify password
            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash);

            // Wrong password
            if (!isPasswordValid)
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password."
                });
            }


            // Claims
            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.Username)
            };


            // Secret key
            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration["Jwt:Key"]));


            // Signing credentials
            var creds =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);


            // Token expiry
            var expires =
                DateTime.Now.AddHours(1);


            // Generate token
            var token =
                new JwtSecurityToken(
                    issuer:
                        _configuration["Jwt:Issuer"],

                    audience:
                        _configuration["Jwt:Audience"],

                    claims: claims,

                    expires: expires,

                    signingCredentials: creds
                );


            return Ok(new
            {
                token =
                    new JwtSecurityTokenHandler()
                    .WriteToken(token),

                expires_in = 3600,

                user = new
                {
                    username = user.Username
                }
            });
        }
    }
}