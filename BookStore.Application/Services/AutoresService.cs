using AutoMapper;
using BookStore.Application.Interface;
using BookStore.Domain.Entities.Model;
using BookStore.Domain.Structs;
using BookStore.Infrastructure.Interfaces;
using X.PagedList;

namespace BookStore.Application.Services;

public class AutoresService : IAutoresService
{
    private readonly IAutoresRepository _repository;
    private readonly IMapper _mapper;

    public AutoresService(IAutoresRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ServiceResult<AutoresDTO>> AdicionarAutorRepositoryService(AutoresDTO autores)
    {
        try
        {
            var autorEntity = _mapper.Map<Autores>(autores);
            var autorCriado = await _repository.AdicionarAutorRepository(autorEntity);

            if (!autorCriado.IsSuccess)
            {
                return ServiceResult<AutoresDTO>.Error(autorCriado.ErrorMessage);
            }

            var autorDTORetornado = _mapper.Map<AutoresDTO>(autorCriado.Data);
            return ServiceResult<AutoresDTO>.Success(autorDTORetornado);
        }
        catch (Exception ex)
        {
            return ServiceResult<AutoresDTO>.Error(ex.Message);
        }
    }

    public async Task<ServiceResult<AutoresDTO>> ProcuraAutorPorIDService(long id)
    {
        try
        {
            var encontraID = await _repository.ProcuraAutorPorID(id);

            if (!encontraID.IsSuccess || encontraID.Data == null)
            {
                return ServiceResult<AutoresDTO>.Error("Autor não encontrado");
            }

            var retornoDTO = _mapper.Map<AutoresDTO>(encontraID.Data);
            return ServiceResult<AutoresDTO>.Success(retornoDTO);
        }
        catch (Exception ex)
        {
            return ServiceResult<AutoresDTO>.Error(ex.Message);
        }
    }

    public async Task<ServiceResult<AutoresDTO>> AtualizaAutorPorIDService(long id, AutoresDTO autores)
    {
        try
        {
            var autorExistente = await _repository.ProcuraAutorPorID(id);

            if (!autorExistente.IsSuccess || autorExistente.Data == null)
            {
                return ServiceResult<AutoresDTO>.Error("Autor não encontrado");
            }

            var mapearAutor = _mapper.Map<Autores>(autores);
            var autorAtualizado = await _repository.AtualizaAutorPorID(id, mapearAutor);

            if (!autorAtualizado.IsSuccess)
            {
                return ServiceResult<AutoresDTO>.Error(autorAtualizado.ErrorMessage);
            }

            var autorRetornado = _mapper.Map<AutoresDTO>(autorAtualizado.Data);
            return ServiceResult<AutoresDTO>.Success(autorRetornado);
        }
        catch (Exception ex)
        {
            return ServiceResult<AutoresDTO>.Error(ex.Message);
        }
    }

    public async Task<ServiceResult<IPagedList<AutoresDTO>>> MostrarTodosOsAutoresService(int numeroDaPagina, int tamanhoDaPagina)
    {
        try
        {
            var mostrarAutores = await _repository.MostrarTodosOsAutores(numeroDaPagina, tamanhoDaPagina);

            if (!mostrarAutores.IsSuccess || mostrarAutores.Data == null || !mostrarAutores.Data.Any())
            {
                return ServiceResult<IPagedList<AutoresDTO>>.Error("Nenhum autor encontrado");
            }

            var autoresRetornados = _mapper.Map<IPagedList<AutoresDTO>>(mostrarAutores.Data);
            return ServiceResult<IPagedList<AutoresDTO>>.Success(autoresRetornados);
        }
        catch (Exception ex)
        {
            return ServiceResult<IPagedList<AutoresDTO>>.Error(ex.Message);
        }
    }

    public async Task<ServiceResult<AutoresDTO>> RemoveAutorPorIDService(long id)
    {
        try
        {
            var procuraID = await _repository.RemoveAutorPorID(id);

            if (!procuraID.IsSuccess || procuraID.Data == null)
            {
                return ServiceResult<AutoresDTO>.Error("Autor não encontrado ou erro ao remover");
            }

            var converteDTO = _mapper.Map<AutoresDTO>(procuraID.Data);
            return ServiceResult<AutoresDTO>.Success(converteDTO);
        }
        catch (Exception ex)
        {
            return ServiceResult<AutoresDTO>.Error(ex.Message);
        }
    }
}
