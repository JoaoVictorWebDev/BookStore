using BookStore.Application.Interface;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Model;
using BookStore.Domain.Structs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BookStore.API.Controller;

[Route("api/[controller]/v1/Autores")]
[ApiController]
public class AutoresController : ControllerBase
{
    public readonly IAutoresService _service;
    public AutoresController(IAutoresService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("AdicionarAutor")]
    public async Task<ActionResult<AutoresDTO>> AdicionarAutor([FromBody] AutoresDTO autores)
    {
        var resultado = await _service.AdicionarAutorRepositoryService(autores);
        if (!resultado.IsSuccess)
        {
            return BadRequest(resultado);
        }

        return Ok(resultado);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("ProcuraAutorPorID/{id}")]
    public async Task<ActionResult<AutoresDTO>> ProcuraAutorPorID(long id)
    {
        var resultado = await _service.ProcuraAutorPorIDService(id);
        if (!resultado.IsSuccess)
        {
            return BadRequest(resultado);
        }

        return Ok(resultado);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("AtualizaAutorPorIDService/{id}")]
    public async Task<ActionResult<AutoresDTO>> AtualizaAutorPorIDService(long id, [FromBody] AutoresDTO autores)
    {
        var resultado = await _service.AtualizaAutorPorIDService(id, autores);
        if (!resultado.IsSuccess)
        {
            return BadRequest(resultado);
        }

        return Ok(resultado);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("MostrarTodosOsAutores")]
    public async Task<ActionResult<IPagedList<AutoresDTO>>> MostrarTodosOsAutores(int NumeroDaPagina, int TamanhoDaPagina)
    {
        var resultado = await _service.MostrarTodosOsAutoresService(NumeroDaPagina, TamanhoDaPagina);
        if (!resultado.IsSuccess)
        {
            return BadRequest(resultado);
        }
        return Ok(resultado);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("RemoveAutorPorID")]
    public async Task<ActionResult<AutoresDTO>> RemoveAutorPorIDService(long id)
    {
        var resultado = await _service.RemoveAutorPorIDService(id);
        if (!resultado.IsSuccess)
        {
            return BadRequest(resultado);
        }

        return Ok(resultado);
    }

}
