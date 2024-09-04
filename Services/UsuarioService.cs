using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinimalAPI.Data;
using MinimalAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalAPI.Services
{
    public class UsuarioService
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public UsuarioService(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Usuario>> ListaUsuarios()
        {
            return await _context.Usuario.ToListAsync();
        }
    }
}
