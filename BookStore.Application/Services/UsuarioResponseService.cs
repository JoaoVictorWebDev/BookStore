using AutoMapper;
using BookStore.Application.Interface;
using BookStore.Domain.Entities.Model;
using BookStore.Domain.Structs;
using BookStore.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BookStore.Application.Services;

public class UsuarioResponseService : IUsuarioResponseService
{
    private readonly IMapper _mapper;
    private readonly IUsuarioResponseRepository _repository;
    public UsuarioResponseService(IUsuarioResponseRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<ServiceResult<IPagedList<UsuarioResponse>>> MostrarTodosUsuariosService(int NumeroDaPagina, int TamanhoDaPagina)
    {
        try
        {
            var usuariosResult = await _repository.MostrarTodosUsuariosRepository(NumeroDaPagina, TamanhoDaPagina);

            if (!usuariosResult.IsSuccess || usuariosResult.Data is null || !usuariosResult.Data.Any())
            {
                return ServiceResult<IPagedList<UsuarioResponse>>.Error("Nenhum usuário encontrado.");
            }

            var usuariosResponse = usuariosResult.Data.Select(usuario => new UsuarioResponse
            {
                Id = usuario.Id,
                NomeDeUsuario = usuario.NomeDeUsuario,
                Email = usuario.Email,
            }).ToPagedList();

            return ServiceResult<IPagedList<UsuarioResponse>>.Success(usuariosResponse);
        }
        catch (Exception ex)
        {
            return ServiceResult<IPagedList<UsuarioResponse>>.Error($"Erro ao buscar usuários: {ex.Message}");
        }
    }

    public async Task<ServiceResult<UsuarioResponse>> ProcurarUsuarioPorNomeService(string procurarUsuarioPorNome)
    {
        try
        {
            var usuarioResult = await _repository.ProcurarUsuarioPorNomeRepository(procurarUsuarioPorNome);

            if (!usuarioResult.IsSuccess || usuarioResult.Data is null)
            {
                return ServiceResult<UsuarioResponse>.Error("Usuário não encontrado.");
            }

            var usuarioResponse = _mapper.Map<UsuarioResponse>(usuarioResult.Data);
            return ServiceResult<UsuarioResponse>.Success(usuarioResponse);
        }
        catch (Exception ex)
        {
            return ServiceResult<UsuarioResponse>.Error($"Erro ao procurar usuário por nome: {ex.Message}");
        }
    }

    public async Task<ServiceResult<UsuarioResponse>> ProcuraUsuarioPorIDService(long id)
    {
        try
        {
            var usuarioResult = await _repository.ProcuraUsuarioPorIDRepository(id);

            if (!usuarioResult.IsSuccess || usuarioResult.Data is null)
            {
                return ServiceResult<UsuarioResponse>.Error("Usuário não encontrado.");
            }

            var usuarioResponse = _mapper.Map<UsuarioResponse>(usuarioResult.Data);
            return ServiceResult<UsuarioResponse>.Success(usuarioResponse);
        }
        catch (Exception ex)
        {
            return ServiceResult<UsuarioResponse>.Error($"Erro ao buscar usuário por ID: {ex.Message}");
        }
    }
}
