using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using MinimalAPI.Data;
using MinimalAPI.Models;
using MinimalAPI.ViewModels;

namespace MinimalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Context _context;
        private IConfiguration _config;

        public AuthenticationController(Context context, IConfiguration Configuration)
        {
            _context = context;
            _config = Configuration;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            // Valida login
            var usuario = await _context.Usuario.FirstOrDefaultAsync(e => e.Email == login.Email && e.Senha == login.Senha);

            if (usuario != null)
            {
                // Gera token
                var _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var _issuer = _config["Jwt:Issuer"];
                var _audience = _config["Jwt:Audience"];
                var _expiresMinutes = int.Parse(_config["Jwt:ExpireMinutes"]);

                var signinCredentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: _issuer,
                    audience: _audience,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(_expiresMinutes),
                    signingCredentials: signinCredentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}

