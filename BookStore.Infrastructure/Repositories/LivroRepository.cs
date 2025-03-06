using BookStore.Domain.Entities.Model;
using BookStore.Domain.Enum;
using BookStore.Domain.Structs;
using BookStore.Infrastructure.Contexts;
using BookStore.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BookStore.Infrastructure.Repositories;

public class LivroRepository : ILivroRepository
{
    private readonly ApplicationDBContext _context;
    public LivroRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<Livros>> AdicionarLivrosRepository(Livros livros)
    {
        if (livros == null)
        {
            return ServiceResult<Livros>.Error("Autor está nulo!");
        }

        try
        {
            livros.DataDePublicacao = DateTime.SpecifyKind(livros.DataDePublicacao, DateTimeKind.Utc);

            _context.Add(livros);
            await _context.SaveChangesAsync();

            return ServiceResult<Livros>.Success(livros);
        }
        catch (Exception ex)
        {
            return ServiceResult<Livros>.Error($"Erro inesperado ao adicionar autor: {ex.Message}");
        }
    }

    public async Task<ServiceResult<Livros>> AtualizarLivrosPorID(long id, Livros livro)
    {
        try
        {
            var PegarLivroPorID = _context.Livros.
                FirstOrDefault(i => i.Id == id);

            if (PegarLivroPorID == null)
            {
                return ServiceResult<Livros>.Error("Autor Não Encontrado");
            }

            PegarLivroPorID.Autor = livro.Autor;
            PegarLivroPorID.DataDePublicacao = livro.DataDePublicacao;
            PegarLivroPorID.Titulo = livro.Titulo;
            PegarLivroPorID.Categoria = livro.Categoria;
            PegarLivroPorID.Quantidade = livro.Quantidade;
            PegarLivroPorID.Price = livro.Price;
            await _context.SaveChangesAsync();
            return ServiceResult<Livros>.Success(PegarLivroPorID);

        }
        catch (Exception ex)
        {
            return ServiceResult<Livros>.Error(ex.Message);
        }
    }

    public async Task<ServiceResult<IPagedList<Livros>>> MostrarTodosOsLivros(int NumeroDaPagina, int TamanhoDaPagina)
    {
        try
        {
            var Livros = await _context.Livros.ToPagedListAsync(NumeroDaPagina, TamanhoDaPagina);

            if (Livros == null || !Livros.Any())
            {
                return ServiceResult<IPagedList<Livros>>.Error("Nenhum Livro encontrado");
            }
            return ServiceResult<IPagedList<Livros>>.Success(Livros);
        }
        catch (Exception ex)
        {
            return ServiceResult<IPagedList<Livros>>.Error(ex.Message);
        }
    }

    public async Task<ServiceResult<Livros>> ProcuraLivrosPorIDRepository(long id)
    {
        try
        {
            var ProcuraLivro = await _context.Livros.FindAsync(id);

            if (ProcuraLivro == null)
            {
                return ServiceResult<Livros>.Error("Livro não encontrado.");
            }
            return ServiceResult<Livros>.Success(ProcuraLivro);
        }
        catch (Exception ex)
        {
            return ServiceResult<Livros>.Error(ex.Message);
        }
    }

    public async Task<ServiceResult<Livros>> RemoveLivrosRepository(long id)
    {
        try
        {
            var ProcuraLivro = await _context.Livros.FirstOrDefaultAsync(i => i.Id == id);

            if (ProcuraLivro == null)
            {
                return ServiceResult<Livros>.Error("Livro não encontrado.");
            }

            _context.Livros.Remove(ProcuraLivro);
            await _context.SaveChangesAsync();
            return ServiceResult<Livros>.Success(ProcuraLivro);
        }
        catch (Exception ex)
        {
            return ServiceResult<Livros>.Error(ex.Message);
        }
    }

    public async Task<ServiceResult<List<Livros>>> BuscarLivrosPorTituloOuCategoria(string titulo, CategoriaDosLivrosEnum? categoria)
    {
        var query = _context.Livros.AsQueryable();

        if (!string.IsNullOrEmpty(titulo))
        {
            query = query.Where(l => l.Titulo.Contains(titulo));
        }

        if (categoria.HasValue)
        {
            query = query.Where(l => l.Categoria == categoria.Value);
        }

        var livros = await query.ToListAsync();
        return ServiceResult<List<Livros>>.Success(livros);
    }

    public async Task<ServiceResult<List<Livros>>> BuscarLivrosPorAutor(long autorId)
    {
        try
        {
            var livros = await _context.Livros.Where(l => l.AutorID == autorId).ToListAsync();

            if (!livros.Any())
            {
                return ServiceResult<List<Livros>>.Error("Nenhum livro encontrado para este autor.");
            }
            return ServiceResult<List<Livros>>.Success(livros);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<Livros>>.Error($"Erro ao buscar livros do autor: {ex.Message}");
        }
    }

    public async Task<ServiceResult<Livros>> ProcuraLivrosPorTitulo(string titulo)
    {
        var resultado = await _context.Livros.FirstOrDefaultAsync(t => t.Titulo == titulo);

        if (resultado == null)
        {
            return ServiceResult<Livros>.Error("Livro não encontrado.");
        }
        return ServiceResult<Livros>.Success(resultado);
    }

    public async Task<ServiceResult<Livros>> ComprarLivroPorID(long id, int quantidade)
    {
        try
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);

            if (livro == null)
            {
                return ServiceResult<Livros>.Error("Livro não encontrado.");
            }

            if (livro.Quantidade < quantidade)
            {
                return ServiceResult<Livros>.Error("Estoque insuficiente para a compra.");
            }

            livro.Quantidade -= quantidade;
            await _context.SaveChangesAsync();

            return ServiceResult<Livros>.Success(livro);
        }
        catch (Exception ex)
        {
            return ServiceResult<Livros>.Error($"Erro ao comprar livro: {ex.Message}");
        }
    }

    public async Task<ServiceResult<Livros>> ComprarLivroPorTitulo(string titulo, int quantidade)
    {
        try
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(l => l.Titulo == titulo);

            if (livro == null)
            {
                return ServiceResult<Livros>.Error("Livro não encontrado.");
            }

            if (livro.Quantidade < quantidade)
            {
                return ServiceResult<Livros>.Error("Estoque insuficiente para a compra.");
            }

            livro.Quantidade -= quantidade;
            await _context.SaveChangesAsync();

            return ServiceResult<Livros>.Success(livro);
        }
        catch (Exception ex)
        {
            return ServiceResult<Livros>.Error($"Erro ao comprar livro: {ex.Message}");
        }
    }
}
