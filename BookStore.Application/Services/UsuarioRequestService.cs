using AutoMapper;
using BookStore.Application.Interface;
using BookStore.Domain.Entities.Model;
using BookStore.Domain.Structs;
using BookStore.Infrastructure.Interfaces;

namespace BookStore.Application.Services;

public class UsuarioRequestService : IUsuarioRequestService
{
    private readonly IMapper _mapper;
    private readonly IUsuarioRepositoryRequest _repository;

    public UsuarioRequestService(IUsuarioRepositoryRequest repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ServiceResult<UsuarioRequest>> AdicionarUsuarioService(UsuarioRequest usuarioRequest, string role)
    {
        try
        {
            if (usuarioRequest.Cargo != "Admin")
            {
                return ServiceResult<UsuarioRequest>.Error("Apenas administradores podem adicionar usuários.");
            }

            var usuarioEntity = _mapper.Map<Usuario>(usuarioRequest);
            var adicionarUsuario = await _repository.AdicionarUsuarioRepository(usuarioEntity, role);

            if (!adicionarUsuario.IsSuccess || adicionarUsuario.Data is null)
            {
                return ServiceResult<UsuarioRequest>.Error("Erro ao adicionar usuário.");
            }

            var usuarioResponse = _mapper.Map<UsuarioRequest>(adicionarUsuario.Data);
            return ServiceResult<UsuarioRequest>.Success(usuarioResponse);
        }
        catch (Exception ex)
        {
            return ServiceResult<UsuarioRequest>.Error($"Erro ao adicionar usuário: {ex.Message}");
        }
    }

    public async Task<ServiceResult<UsuarioRequest>> AtualizaUsuarioPorIDService(long id, UsuarioRequest usuarioRequest, string role)
    {
        try
        {
            if (usuarioRequest.Cargo != "Admin")
            {
                return ServiceResult<UsuarioRequest>.Error("Apenas administradores podem atualizar usuários.");
            }

            var usuarioEntity = _mapper.Map<Usuario>(usuarioRequest);
            var atualizaUsuario = await _repository.AtualizaUsuarioPorIDRepository(id, usuarioEntity, role);

            if (!atualizaUsuario.IsSuccess || atualizaUsuario.Data is null)
            {
                return ServiceResult<UsuarioRequest>.Error("Erro ao atualizar usuário.");
            }

            var usuarioResponse = _mapper.Map<UsuarioRequest>(atualizaUsuario.Data);
            return ServiceResult<UsuarioRequest>.Success(usuarioResponse);
        }
        catch (Exception ex)
        {
            return ServiceResult<UsuarioRequest>.Error($"Erro ao atualizar usuário: {ex.Message}");
        }
    }

    public async Task<ServiceResult<UsuarioRequest>> RemoveUsuarioPorIDService(long id, UsuarioRequest usuarioRequest, string role)
    {
        try
        {
            if (usuarioRequest.Cargo != "Admin")
            {
                return ServiceResult<UsuarioRequest>.Error("Apenas administradores podem remover usuários.");
            }

            var removeUsuario = await _repository.RemoveUsuarioPorIDRepository(id, role);

            if (!removeUsuario.IsSuccess || removeUsuario.Data is null)
            {
                return ServiceResult<UsuarioRequest>.Error("Erro ao remover usuário.");
            }

            var usuarioResponse = _mapper.Map<UsuarioRequest>(removeUsuario.Data);
            return ServiceResult<UsuarioRequest>.Success(usuarioResponse);
        }
        catch (Exception ex)
        {
            return ServiceResult<UsuarioRequest>.Error($"Erro ao remover usuário: {ex.Message}");
        }
    }
}
