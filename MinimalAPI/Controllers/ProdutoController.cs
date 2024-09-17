using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

using MinimalAPI.Data;
using MinimalAPI.Models;
using MinimalAPI.ViewModels;

namespace MinimalAPI.Controllers
{
    [ApiController, Authorize, Route("api/[controller]")]
    [SwaggerTag("Endpoints relacionados à gestão de produtos. Inclui cadastro, consulta, edição e exclusão de produtos.")]
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
        [SwaggerOperation(Summary = "Lista todos produtos", Description = "Retorna a lista de todos produtos cadastrados.")]
        public async Task<IActionResult> GetProdutos()
        {
            var produtos = await _context.Produto.ToListAsync();
            if (produtos.IsNullOrEmpty())
            {
                return NotFound();
            }
            return new ObjectResult(produtos);
        }

        [HttpGet, Authorize, Route("{id}")]
        [SwaggerOperation(Summary = "Lista produto por id", Description = "Retorna produto por id consultado.")]
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
        [SwaggerOperation(Summary = "Cadastra produto", Description = "Realiza cadastro de um novo produto.")]
        public async Task<IActionResult> PostProduto(Produto produto)
        {
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();
            return Ok("Produto cadastrado com sucesso!");
        }

        [HttpPut, Authorize]
        [SwaggerOperation(Summary = "Altera produto", Description = "Altera cadastro de um produto.")]
        public async Task<IActionResult> PutProduto(Produto produto)
        {
            _context.Produto.Update(produto);
            await _context.SaveChangesAsync();
            return Ok("Produto alterado com sucesso!");
        }

        [HttpDelete, Authorize, Route("{id}")]
        [SwaggerOperation(Summary = "Deleta produto", Description = "Deleta cadastro de um produto.")]
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