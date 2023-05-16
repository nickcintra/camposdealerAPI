using CamposDealer_React.Data;
using CamposDealer_React.Data.DTO_s;
using CamposDealer_React.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CamposDealer_React.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private APIContext _apicontext;

        public ProdutoController(APIContext apicontext)
        {
            _apicontext = apicontext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var produtos = _apicontext.Produtos.ToList();
            return Ok(produtos);
        }

        // GET: api/produtos/1
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var produto = _apicontext.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // POST: api/produtos
        [HttpPost]
        public IActionResult Post([FromBody] ProdutoDTO produtoDTO)
        {
            if (produtoDTO == null)
            {
                return BadRequest("Dados inválidos");
            }

            var produto = new Produto
            {
                Descricao = produtoDTO.Descricao,
                Valor = produtoDTO.Valor
            };

            _apicontext.Produtos.Add(produto);
            _apicontext.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
        }

        // PUT: api/produtos/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Produto produtoAtualizado)
        {
            if (produtoAtualizado == null || produtoAtualizado.Id != id)
            {
                return BadRequest("Dados inválidos");
            }

            var produto = _apicontext.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            produto.Descricao = produtoAtualizado.Descricao;
            produto.Valor = produtoAtualizado.Valor;

            _apicontext.SaveChanges();

            return NoContent();
        }

        // DELETE: api/produtos/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var produto = _apicontext.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            _apicontext.Produtos.Remove(produto);
            _apicontext.SaveChanges();

            return NoContent();
        }
    }
}
