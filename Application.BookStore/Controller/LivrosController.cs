using BookStore.Application.Interface;
using BookStore.Domain.Entities.Model;
using BookStore.Domain.Enum;
using BookStore.Domain.Structs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/livros")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> AdicionarLivro([FromBody] LivrosDTO livro)
        {
            var result = await _livroService.AdicionarLivrosService(livro);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> RemoverLivro(long id)
        {
            var result = await _livroService.RemoveLivrosService(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("buscar/{id}")]
        public async Task<IActionResult> BuscarLivroPorID(long id)
        {
            var result = await _livroService.ProcuraLivrosPorIDService(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarLivros([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
        {
            var result = await _livroService.MostrarTodosOsService(pagina, tamanho);
            return Ok(result);
        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarLivro(long id, [FromBody] LivrosDTO livro)
        {
            var result = await _livroService.AtualizarLivrosPorID(id, livro);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("comprar/id/{id}/quantidade/{quantidade}")]
        public async Task<IActionResult> ComprarLivroPorID(long id, int quantidade)
        {
            var result = await _livroService.ComprarLivroPorID(id, quantidade);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("comprar/titulo/{titulo}/quantidade/{quantidade}")]
        public async Task<IActionResult> ComprarLivroPorTitulo(string titulo, int quantidade)
        {
            var result = await _livroService.ComprarLivroPorTitulo(titulo, quantidade);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("buscar/titulo-ou-categoria")]
        public async Task<IActionResult> BuscarPorTituloOuCategoria([FromQuery] string titulo, [FromQuery] CategoriaDosLivrosEnum? categoriaId)
        {
            var result = await _livroService.BuscarLivrosPorTituloOuCategoriaService(titulo, categoriaId);
            return Ok(result);
        }

        [HttpGet("buscar/autor/{autorId}")]
        public async Task<IActionResult> BuscarPorAutor(long autorId)
        {
            var result = await _livroService.BuscarLivrosPorAutorService(autorId);
            return Ok(result);
        }
    }
}
