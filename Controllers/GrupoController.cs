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
    [Authorize]
    public class GrupoController : ControllerBase
    {
        private readonly Context _context;
        private IConfiguration _config;

        public GrupoController(Context context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetGrupos()
        {
            var grupos = await _context.Grupo.ToListAsync();
            if (grupos == null)
            {
                return NotFound();
            }
            return new ObjectResult(grupos);
        }

        [HttpGet, Authorize, Route("{id}")]
        public async Task<IActionResult> GetGrupo(int? id)
        {
            var grupo = await _context.Grupo.FirstOrDefaultAsync(u => u.Id == id);
            if (grupo == null)
            {
                return NotFound();
            }
            return new ObjectResult(grupo);
        }

        [HttpPost]
        public async Task<IActionResult> PostGrupo(Grupo grupo)
        {
            _context.Grupo.Add(grupo);
            await _context.SaveChangesAsync();
            return Ok("Grupo cadastrado com sucesso!");
        }

        [HttpPut, Authorize]
        public async Task<IActionResult> PutGrupo(Grupo grupo)
        {
            _context.Grupo.Update(grupo);
            await _context.SaveChangesAsync();
            return Ok("Grupo alterado com sucesso!");
        }

        [HttpDelete, Authorize, Route("{id}")]
        public async Task<IActionResult> DeleteGrupo(int? id)
        {
            var grupo = await _context.Grupo.FirstOrDefaultAsync(u => u.Id == id);
            if (grupo == null)
                return NotFound();
            _context.Grupo.Remove(grupo);
            await _context.SaveChangesAsync();
            return Ok("Grupo deletado com sucesso");
        }
    }
}