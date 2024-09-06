using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MinimalAPI.Data;
using MinimalAPI.Models;
using MinimalAPI.ViewModels;

namespace MinimalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly Context _context;
        private IConfiguration _config;

        public UsuarioController(Context context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _context.Usuario.ToListAsync();
            if (usuarios == null)
            {
                return NotFound();
            }
            return new ObjectResult(usuarios);
        }

        //GET usuarios por nome

        [HttpGet, Authorize, Route("{id}")]
        public async Task<IActionResult> GetUsuario(int? id)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return new ObjectResult(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuario(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok("Usuário cadastrado com sucesso!");
        }

        [HttpPut, Authorize]
        public async Task<IActionResult> PutUsuario(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();
            return Ok("Usuário alterado com sucesso!");
        }

        [HttpDelete, Authorize, Route("{id}")]
        public async Task<IActionResult> DeleteUsuario(int? id)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
                return NotFound();
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return Ok("Usuário deletado com sucesso");
        }
    }
}