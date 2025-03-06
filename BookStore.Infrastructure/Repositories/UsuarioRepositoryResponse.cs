using BookStore.Domain.Entities.Model;
using BookStore.Domain.Handler;
using BookStore.Domain.Structs;
using BookStore.Infrastructure.Contexts;
using BookStore.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BookStore.Infrastructure.Repositories;

public class UsuarioRepositoryResponse : IUsuarioResponseRepository
{
    private readonly ApplicationDBContext _context;
    public UsuarioRepositoryResponse(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<ServiceResult<IPagedList<Usuario>>> MostrarTodosUsuariosRepository(int NumeroDaPagina, int TamanhoDaPagina)
    {

        try
        {
            var Usuario = await _context.Usuario.ToPagedListAsync(NumeroDaPagina, TamanhoDaPagina);
            if (Usuario == null || !Usuario.Any())
            {
                return ServiceResult<IPagedList<Usuario>>.Error("Nenhum Usuario encontrada");
            }
            return ServiceResult<IPagedList<Usuario>>.Success(Usuario);
        }
        catch (Exception ex)
        {
            return ServiceResult<IPagedList<Usuario>>.Error(ex.Message);
        }
    }

    public async Task<ServiceResult<Usuario>> ProcurarUsuarioPorNomeRepository(string procurarUsuarioPorNome)
    {
        try
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.NomeDeUsuario == procurarUsuarioPorNome);
            if (usuario == null)
            {
                return ServiceResult<Usuario>.Error("Usuário não encontrado");
            }
            return ServiceResult<Usuario>.Success(usuario);
        }
        catch (Exception ex)
        {
            return ServiceResult<Usuario>.Error(ex.Message);
        }
    }


    public async Task<ServiceResult<Usuario>> ProcuraUsuarioPorIDRepository(long id)
    {
        try
        {
            var ProcuraUsuario = await _context.Usuario.FindAsync(id);
            return ServiceResult<Usuario>.Success(ProcuraUsuario);
        }
        catch (Exception ex)
        {
            return ServiceResult<Usuario>.Error(ex.Message);
        }
    }
}
