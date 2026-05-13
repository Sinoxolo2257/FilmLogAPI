using Microsoft.AspNetCore.Mvc;
using FilmLogAPI.Models;
using FilmLogAPI.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace FilmLogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public AuthController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        // POST /api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto request)
        {
            // Check if email already exists
            if (_context.Users.Any(u => u.Email == request.Email))
                return BadRequest(new { message = "Email already registered" });

            var user = new User
            {
                Email = request.Email,
                PasswordHashed = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "Registration successful" });
        }

        // POST /api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] RegisterDto request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHashed))
                return Unauthorized(new { message = "Invalid email or password" });

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.UserId.ToString()) // needed for protected routes
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}