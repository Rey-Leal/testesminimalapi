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
    public class ProdutoController : ControllerBase
    {
        private readonly Context _context;
        private IConfiguration _config;

        public ProdutoController(Context context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetProdutos()
        {
            var produtos = await _context.Produto.ToListAsync();
            if (produtos == null)
            {
                return NotFound();
            }
            return new ObjectResult(produtos);
        }

        [HttpGet, Authorize, Route("{id}")]
        public async Task<IActionResult> GetProduto(int? id)
        {
            var produto = await _context.Produto.FirstOrDefaultAsync(u => u.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            return new ObjectResult(produto);
        }

        [HttpPost]
        public async Task<IActionResult> PostProduto(Produto produto)
        {
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();
            return Ok("Produto cadastrado com sucesso!");
        }

        [HttpPut, Authorize]
        public async Task<IActionResult> PutProduto(Produto produto)
        {
            _context.Produto.Update(produto);
            await _context.SaveChangesAsync();
            return Ok("Produto alterado com sucesso!");
        }

        [HttpDelete, Authorize, Route("{id}")]
        public async Task<IActionResult> DeleteProduto(int? id)
        {
            var produto = await _context.Produto.FirstOrDefaultAsync(u => u.Id == id);
            if (produto == null)
                return NotFound();
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();
            return Ok("Produto deletado com sucesso");
        }
    }
}