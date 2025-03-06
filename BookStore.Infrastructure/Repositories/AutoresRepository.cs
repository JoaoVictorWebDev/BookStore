using BookStore.Domain.Entities.Model;
using BookStore.Domain.Structs;
using BookStore.Infrastructure.Contexts;
using BookStore.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
namespace BookStore.Infrastructure.Repositories;
public class AutoresRepository: IAutoresRepository
{
    private readonly ApplicationDBContext _context;
    public AutoresRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<Autores>> AdicionarAutorRepository(Autores autores)
    {
        if (autores == null)
        {
            return ServiceResult<Autores>.Error("Autor está nulo!");
        }

        try
        {
            autores.DataNascimento = DateTime.SpecifyKind(autores.DataNascimento, DateTimeKind.Utc);

            _context.Add(autores);
            await _context.SaveChangesAsync();

            return ServiceResult<Autores>.Success(autores);
        }
        catch (Exception ex)
        {
            return ServiceResult<Autores>.Error($"Erro inesperado ao adicionar autor: {ex.Message}");
        }
    }

    public async Task<ServiceResult<Autores>> AtualizaAutorPorID(long id, Autores autores)
    {
        try 
        { 
            var PegarIDAutor =  _context.Autores.
                FirstOrDefault(i => i.Id == id);
        
            if(PegarIDAutor == null)
            {
                return ServiceResult<Autores>.Error("Autor Não Encontrado");
            }

            //PegarIDAutor.Livros = autores.Livros;
            PegarIDAutor.Pais = autores.Pais;
            PegarIDAutor.Nome = autores.Nome;
            PegarIDAutor.DataNascimento = autores.DataNascimento;
            await _context.SaveChangesAsync();
            return ServiceResult<Autores>.Success(PegarIDAutor);

        }catch(Exception ex)
        {
            return ServiceResult<Autores>.Error(ex.Message);
        }
    }

    public async Task <ServiceResult<IPagedList<Autores>>> MostrarTodosOsAutores(int NumeroDaPagina, int TamanhoDaPagina)
    {
        try
        {
            var autores = await _context.Autores.ToPagedListAsync(NumeroDaPagina, TamanhoDaPagina);
            if(autores == null || autores.Any())
            {
                return ServiceResult<IPagedList<Autores>>.Error("Nenhum autor encontrado");
            }
            return ServiceResult<IPagedList<Autores>>.Success(autores);
        }catch(Exception ex)
        {
            return ServiceResult<IPagedList<Autores>>.Error(ex.Message);
        }
    }

    public async Task<ServiceResult<Autores>> ProcuraAutorPorID(long id)
    {
        try
        {
            var ProcuraAutor = await _context.Autores.FindAsync(id);
            return ServiceResult<Autores>.Success(ProcuraAutor);
        }catch(Exception ex)
        {
            return ServiceResult<Autores>.Error(ex.Message);
        }
    }

    public async Task<ServiceResult<Autores>> RemoveAutorPorID(long id)
    {
        try
        {
            var EncontraAutor = await _context.Autores.
                FirstOrDefaultAsync(i => i.Id == id);
            _context.Autores.Remove(EncontraAutor);
            _context.SaveChangesAsync();
            return ServiceResult<Autores>.Success(EncontraAutor);
        }catch(Exception ex)
        {
            return ServiceResult<Autores>.Error(ex.Message);
        }
    }
}
