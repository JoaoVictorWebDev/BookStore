using BookStore.Domain.Entities.Model;
using BookStore.Domain.Handler;
using BookStore.Domain.Structs;
using BookStore.Infrastructure.Contexts;
using BookStore.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BookStore.Infrastructure.Repositories;
public class UsuarioRepositoryRequest : IUsuarioRepositoryRequest
{
    private readonly ApplicationDBContext _context;
    private readonly PasswordHandler _passwordHandler;

    public UsuarioRepositoryRequest(ApplicationDBContext context, PasswordHandler passwordHandler)
    {
        _context = context;
        _passwordHandler = passwordHandler;
    }

    public async Task<ServiceResult<Usuario>> AdicionarUsuarioRepository(Usuario usuario, string role)
    {
        try
        {
            if (role != "Admin")
            {
                return ServiceResult<Usuario>.Error("Apenas administradores podem adicionar usuários.");
            }

            if (usuario == null)
            {
                return ServiceResult<Usuario>.Error("O usuário está nulo.");
            }

            usuario.Senha = _passwordHandler.Hash(usuario.Senha);
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return ServiceResult<Usuario>.Success(usuario);
        }
        catch (Exception ex)
        {
            return ServiceResult<Usuario>.Error($"Erro ao adicionar usuário: {ex.Message}");
        }
    }

    public async Task<ServiceResult<Usuario>> AtualizaUsuarioPorIDRepository(long id, Usuario usuario, string role)
    {
        try
        {
            if (role != "Admin")
            {
                return ServiceResult<Usuario>.Error("Apenas administradores podem atualizar usuários.");
            }

            var usuarioExistente = await _context.Usuario.FirstOrDefaultAsync(i => i.Id == id);

            if (usuarioExistente == null)
            {
                return ServiceResult<Usuario>.Error("Usuário não encontrado.");
            }

            usuarioExistente.Email = usuario.Email;
            usuarioExistente.NomeDeUsuario = usuario.NomeDeUsuario;

            if (!string.IsNullOrEmpty(usuario.Senha))
            {
                usuarioExistente.Senha = _passwordHandler.Hash(usuario.Senha);
            }

            await _context.SaveChangesAsync();
            return ServiceResult<Usuario>.Success(usuarioExistente);
        }
        catch (Exception ex)
        {
            return ServiceResult<Usuario>.Error(ex.Message);
        }
    }

    public async Task<ServiceResult<Usuario>> RemoveUsuarioPorIDRepository(long id, string role)
    {
        try
        {
            if (role != "Admin")
            {
                return ServiceResult<Usuario>.Error("Apenas administradores podem remover usuários.");
            }

            var usuarioEncontrado = await _context.Usuario.FirstOrDefaultAsync(i => i.Id == id);

            if (usuarioEncontrado == null)
            {
                return ServiceResult<Usuario>.Error("Usuário não encontrado.");
            }

            _context.Usuario.Remove(usuarioEncontrado);
            await _context.SaveChangesAsync();
            return ServiceResult<Usuario>.Success(usuarioEncontrado);
        }
        catch (Exception ex)
        {
            return ServiceResult<Usuario>.Error($"Não foi possível realizar a remoção: {ex.Message}");
        }
    }
}
