using CamposDealer_React.Data;
using CamposDealer_React.Data.DTO_s;
using CamposDealer_React.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using CamposDealer_React.Requests;
using AutoMapper;
using CamposDealer_React.Response;
using Newtonsoft.Json;

namespace CamposDealer_React.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendaController : ControllerBase
{
    private APIContext _apicontext;
    private IMapper _mapper;

    public VendaController(APIContext apicontext, IMapper mapper)
    {
        _apicontext = apicontext;
        _mapper = mapper;
    }


    // GET: api/vendas/listar-todas
    [HttpGet]
    public IActionResult ListarTodasVendas()
    {
        var vendasDb = _apicontext.Vendas
            .Include(v => v.Cliente)
            .Include(v => v.VendaProdutos)
            .ThenInclude(vp => vp.Produto)
            .ToList();

        var vendasDTO = new List<VendaDTO>();

        foreach (var vendaDb in vendasDb)
        {
            var vendaDTO = new VendaDTO
            {
                DataVenda = vendaDb.DataVenda,
                ValorTotal = vendaDb.ValorTotal,
                ClienteId = vendaDb.Cliente.Id
            };

            vendasDTO.Add(vendaDTO);
        }

        return Ok(vendasDTO);
    }

    // GET: api/vendas/1
    [HttpGet("{id}")]
    public IActionResult ListarVendaPorId(int id)
    {
        var venda = _apicontext.Vendas
        .Include(v => v.Cliente)
        .Include(v => v.VendaProdutos)
        .ThenInclude(vp => vp.Produto)
        .FirstOrDefault(v => v.Id == id);

        if (venda == null)
        {
            return NotFound();
        }

        var vendaResponse = new VendaResponse
        {
            Id = venda.Id,
            DataVenda = venda.DataVenda,
            ValorTotal = venda.ValorTotal,
            ClienteId = venda.ClienteId,
            Produtos = venda.VendaProdutos.Select(vp => new ProdutoResponse
            {
                Id = vp.Produto.Id,
                Descricao = vp.Produto.Descricao,
                Valor = vp.Produto.Valor,
                Quantidade = vp.Quantidade
            }).ToList()
        };

        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        var json = JsonConvert.SerializeObject(vendaResponse, settings);

        return Content(json,"application/json");
    }

    // POST: api/vendas
    [HttpPost]
    public IActionResult InserirVenda([FromBody] VendaRequest vendaRequest)
    {
        if (vendaRequest == null || vendaRequest.Produtos == null || !vendaRequest.Produtos.Any())
        {
            return BadRequest("Dados inválidos");
        }

        var cliente = _apicontext.Clientes.FirstOrDefault(c => c.Id == vendaRequest.ClienteId);

        if (cliente == null)
        {
            return BadRequest($"Cliente com ID {vendaRequest.ClienteId} não encontrado.");
        }

        var venda = new Venda
        {
            DataVenda = DateTime.Now,
            Cliente = cliente,
            VendaProdutos = new List<VendaProduto>()
        };

        double valorTotal = 0;

        foreach (var produtoVendaDTO in vendaRequest.Produtos)
        {
            var produto = _apicontext.Produtos.FirstOrDefault(p => p.Id == produtoVendaDTO.ProdutoId);

            if (produto == null)
            {
                return BadRequest($"Produto com ID {produtoVendaDTO.ProdutoId} não encontrado.");
            }

            var vendaProduto = new VendaProduto
            {
                Venda = null,
                Produto = produto,
                Quantidade = produtoVendaDTO.Quantidade
            };

            venda.VendaProdutos.Add(vendaProduto);

            valorTotal += produto.Valor * produtoVendaDTO.Quantidade;
        }

        venda.ValorTotal = valorTotal;

        _apicontext.Vendas.Add(venda);
        _apicontext.SaveChanges();

       

        var json = JsonConvert.SerializeObject(venda);

        return Ok(json);
    }

    // PUT: api/vendas/1
    [HttpPut("{id}")]
    public IActionResult AtualizarVenda(int id, [FromBody] VendaRequest vendaRequest)
    {
        if (vendaRequest == null || vendaRequest.Produtos == null || !vendaRequest.Produtos.Any())
        {
            return BadRequest("Dados inválidos");
        }

        var venda = _apicontext.Vendas.Include(v => v.VendaProdutos).FirstOrDefault(v => v.Id == id);

        if (venda == null)
        {
            return NotFound();
        }

        venda.Cliente = _apicontext.Clientes.FirstOrDefault(c => c.Id == vendaRequest.ClienteId);
        venda.VendaProdutos.Clear();

        double valorTotal = 0;

        foreach (var produtoDTO in vendaRequest.Produtos)
        {
            var produto = _apicontext.Produtos.FirstOrDefault(p => p.Id == produtoDTO.ProdutoId);

            if (produto == null)
            {
                return BadRequest($"Produto com ID {produtoDTO.ProdutoId} não encontrado.");
            }

            venda.VendaProdutos.Add(new VendaProduto
            {
                Venda = venda,
                Produto = produto,
                Quantidade = produtoDTO.Quantidade
            });

            valorTotal += produto.Valor * produtoDTO.Quantidade;
        }

        venda.ValorTotal = valorTotal;

        _apicontext.SaveChanges();

        return Ok(venda);
    }

    // DELETE: api/vendas/1
    [HttpDelete("{id}")]
    public IActionResult ExcluirVenda(int id)
    {
        var venda = _apicontext.Vendas.Include(v => v.VendaProdutos).FirstOrDefault(v => v.Id == id);

        if (venda == null)
        {
            return NotFound();
        }

        _apicontext.Vendas.Remove(venda);
        _apicontext.SaveChanges();

        return Ok(venda);
    }
}

