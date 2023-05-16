using AutoMapper;
using CamposDealer_React.Data;
using CamposDealer_React.Data.DTO_s;
using CamposDealer_React.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CamposDealer_React.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private APIContext _apicontext;
    private IMapper _mapper;

    public ClienteController(APIContext apicontext, IMapper mapper)
    {
        _apicontext = apicontext;
        _mapper = mapper;
    }

    // GET: api/clientes
    [HttpGet]
    public IActionResult ListarClientes()
    {
        var clientes = _apicontext.Clientes.ToList();
        return Ok(clientes);
    }

    // GET: api/clientes/1
    [HttpGet("{id}")]
    public IActionResult ListarClientePorId(int id)
    {
        var cliente = _apicontext.Clientes.FirstOrDefault(c => c.Id == id);

        if (cliente == null)
        {
            return NotFound();
        }

        return Ok(cliente);
    }

    // POST: api/clientes
    [HttpPost]
    public IActionResult CadastroCliente([FromBody] ClienteDTO clienteDTO)
    {
        if (clienteDTO == null)
        {
            return BadRequest("Dados inválidos");
        }

        var cliente = new Cliente
        {
            Nome = clienteDTO.Nome,
            Cidade = clienteDTO.Cidade
        };

        _apicontext.Clientes.Add(cliente);
        _apicontext.SaveChanges();

        return CreatedAtAction(nameof(ListarClientePorId), new { id = cliente.Id }, cliente);
    }

    // PUT: api/clientes/1
    [HttpPut("{id}")]
    public IActionResult AlterarCliente(int id, [FromBody] ClienteDTO clienteDTO)
    {
        if (clienteDTO == null)
        {
            return BadRequest("Dados inválidos");
        }

        var cliente = _apicontext.Clientes.FirstOrDefault(c => c.Id == id);

        if (cliente == null)
        {
            return NotFound();
        }

        cliente.Nome = clienteDTO.Nome;
        cliente.Cidade = clienteDTO.Cidade;

        _apicontext.SaveChanges();

        return NoContent();
    }

    // DELETE: api/clientes/1
    [HttpDelete("{id}")]
    public IActionResult DeletaCliente(int id)
    {
        var cliente = _apicontext.Clientes.FirstOrDefault(c => c.Id == id);

        if (cliente == null)
        {
            return NotFound();
        }

        _apicontext.Clientes.Remove(cliente);
        _apicontext.SaveChanges();

        return NoContent();
    }
}