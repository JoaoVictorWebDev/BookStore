using BookStore.Application.Interface;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Model;
using BookStore.Domain.Structs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
namespace BookStore.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Microsoft.AspNetCore.Mvc.ControllerBase
{
    public readonly IJWTService _jwtService;
    public readonly IUsuarioRequestService usuarioRequestService;
    public readonly IUsuarioResponseService usuarioResponse;

    public AccountController(IJWTService jwtService, IUsuarioResponseService response, IUsuarioRequestService request)
    {
        _jwtService = jwtService;
        usuarioRequestService = request;
        usuarioResponse = response;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<UsuarioResponse>> Login([FromBody] UsuarioRequest request)
    {
        var result = await _jwtService.Authenticate(request);

        if(result is null)
        {
            return Unauthorized();
        }
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("Registrar")]
    public async Task<ActionResult<UsuarioRequest>> RegistrarUsuario([FromBody] UsuarioRequest request, string role)
    {
        var result = await usuarioRequestService.AdicionarUsuarioService(request, role);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("AtualizarUsuarioPorID/{id}")]
    public async Task<ActionResult<UsuarioRequest>> AtualizaUsuarioPorIDService([FromRoute]long id, UsuarioRequest usuarioRequest, string role)
    {
        var resultado = await usuarioRequestService.AtualizaUsuarioPorIDService(id, usuarioRequest, role);
        return Ok(resultado);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("RemoveFuncionarioPorID/{id}")]
    public async Task<ActionResult<UsuarioRequest>> RemoveUsuarioPorIDService([FromRoute]long id, UsuarioRequest usuarioRequest, string role)
    {
        var resultado = await usuarioRequestService.RemoveUsuarioPorIDService(id, usuarioRequest, role);
        return Ok(resultado);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("ProcurarUsuarioPorID/{id}")]
    public async Task<ActionResult<UsuarioResponse>> ProcuraUsuarioPorID([FromRoute] long id)
    {
        var resultado = await usuarioResponse.ProcuraUsuarioPorIDService(id);
        return Ok(resultado);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("MostrarTodosOsUsuarios")]
    public async Task<ActionResult<IPagedList<UsuarioResponse>>> MostrarTodosUsuariosService([FromQuery] int NumeroDaPagina, int TamanhoDaPagina)
    {
        var resultado = await usuarioResponse.MostrarTodosUsuariosService(NumeroDaPagina, TamanhoDaPagina);
        return Ok(resultado);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("ProcuraUsuarioPorNome")]
    public async Task<ActionResult<UsuarioResponse>> ProcurarUsuarioPorNomeService(string procurarUsuarioPorNome)
    {
        var resultado = await usuarioResponse.ProcurarUsuarioPorNomeService(procurarUsuarioPorNome);
        return Ok(resultado);
    }
}
