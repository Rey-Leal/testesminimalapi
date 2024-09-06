using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

using MinimalAPI.Data;
using MinimalAPI.Models;
using MinimalAPI.ViewModels;

namespace MinimalAPI.Controllers
{
    [ApiController, Authorize, Route("api/[controller]")]
    [SwaggerTag("Endpoints relacionados à gestão de grupos. Inclui cadastro, consulta, edição e exclusão de grupos.")]
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
        [SwaggerOperation(Summary = "Lista todos grupos", Description = "Retorna a lista de todos grupos cadastrados.")]
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
        [SwaggerOperation(Summary = "Lista grupo por id", Description = "Retorna grupo por id consultado.")]
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
        [SwaggerOperation(Summary = "Cadastra grupo", Description = "Realiza cadastro de um novo grupo.")]
        public async Task<IActionResult> PostGrupo(Grupo grupo)
        {
            _context.Grupo.Add(grupo);
            await _context.SaveChangesAsync();
            return Ok("Grupo cadastrado com sucesso!");
        }

        [HttpPut, Authorize]
        [SwaggerOperation(Summary = "Altera grupo", Description = "Altera cadastro de um grupo.")]
        public async Task<IActionResult> PutGrupo(Grupo grupo)
        {
            _context.Grupo.Update(grupo);
            await _context.SaveChangesAsync();
            return Ok("Grupo alterado com sucesso!");
        }

        [HttpDelete, Authorize, Route("{id}")]
        [SwaggerOperation(Summary = "Deleta grupo", Description = "Deleta cadastro de um grupo.")]
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