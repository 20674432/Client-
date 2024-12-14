using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClientPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ClientPortalContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ClientPortalContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Client>> Register(Client client)
        {
            // Ensure the username and email are unique
            if (await _context.Clients.AnyAsync(c => c.Username == client.Username || c.Email == client.Email))
            {
                return BadRequest("Username or Email already exists.");
            }

            // Hash the password (use a proper method for password storage in production)
            client.PasswordHash = BCrypt.Net.BCrypt.HashPassword(client.PasswordHash);
            client.CreatedAt = DateTime.UtcNow;

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return Ok(GenerateJwtToken(client));
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel loginRequest)
        {
            var client = await _context.Clients.SingleOrDefaultAsync(c => c.Username == loginRequest.Username);

            if (client == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, client.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(GenerateJwtToken(client));
        }
        private string GenerateJwtToken(Client client)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, client.Username),
                new Claim(JwtRegisteredClaimNames.Email, client.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:TokenLifetimeMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
