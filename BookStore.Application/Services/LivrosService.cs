using AutoMapper;
using BookStore.Application.Interface;
using BookStore.Domain.Entities.Model;
using BookStore.Domain.Enum;
using BookStore.Domain.Structs;
using BookStore.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace BookStore.Application.Services;

public class LivrosService : ILivroService
{
    private readonly ILivroRepository _repository;
    private readonly IMapper _mapper;

    public LivrosService(ILivroRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ServiceResult<LivrosDTO>> AdicionarLivrosService(LivrosDTO livros)
    {
        try
        {
            var livroModel = _mapper.Map<Livros>(livros);
            var livroAdicionado = await _repository.AdicionarLivrosRepository(livroModel);
            if (!livroAdicionado.IsSuccess)
            {
                return ServiceResult<LivrosDTO>.Error(livroAdicionado.ErrorMessage);
            }
            var LivroDTORetornado = _mapper.Map<LivrosDTO>(livroAdicionado.Data);
            return ServiceResult<LivrosDTO>.Success(LivroDTORetornado);
        }
        catch (Exception ex)
        {
            return ServiceResult<LivrosDTO>.Error(ex.Message);
        }

    }

    public async Task<ServiceResult<LivrosDTO>> RemoveLivrosService(long id)
    {
        var livro = await _repository.ProcuraLivrosPorIDRepository(id);
        if (!livro.IsSuccess || livro.Data == null)
        {
            return ServiceResult<LivrosDTO>.Error("Livro não encontrado.");
        }

        var removeLivros = await _repository.RemoveLivrosRepository(id);
        return ServiceResult<LivrosDTO>.Success(_mapper.Map<LivrosDTO>(livro.Data));
    }

    public async Task<ServiceResult<LivrosDTO>> ProcuraLivrosPorIDService(long id)
    {
        try
        {
            var livro = await _repository.ProcuraLivrosPorIDRepository(id);
            if (!livro.IsSuccess || livro.Data == null)
            {
                return ServiceResult<LivrosDTO>.Error("Livro não encontrado.");
            }
            var retornoDTO = _mapper.Map<LivrosDTO>(livro.Data);
            return ServiceResult<LivrosDTO>.Success(retornoDTO);
        }
        catch (Exception ex)
        {
            return ServiceResult<LivrosDTO>.Error(ex.Message);

        }

    }

    public async Task<ServiceResult<IPagedList<LivrosDTO>>> MostrarTodosOsService(int numeroDaPagina, int tamanhoDaPagina)
    {
        try
        {
            var mostrarLivros = await _repository.MostrarTodosOsLivros(numeroDaPagina, tamanhoDaPagina);

            if (!mostrarLivros.IsSuccess || mostrarLivros.Data == null || !mostrarLivros.Data.Any())
            {
                return ServiceResult<IPagedList<LivrosDTO>>.Error("Nenhum autor encontrado");
            }
            var livrosDTO = _mapper.Map<List<LivrosDTO>>(mostrarLivros.Data);
            var autoresRetornados = livrosDTO.ToPagedList(numeroDaPagina, tamanhoDaPagina);
            return ServiceResult<IPagedList<LivrosDTO>>.Success(autoresRetornados);
        }
        catch (Exception ex)
        {
            return ServiceResult<IPagedList<LivrosDTO>>.Error(ex.Message);
        }
    }

    public async Task<ServiceResult<LivrosDTO>> AtualizarLivrosPorID(long id, LivrosDTO livros)
    {
        try
        {
            var LivroExistente = await _repository.ProcuraLivrosPorIDRepository(id);

            if (!LivroExistente.IsSuccess || LivroExistente.Data == null)
            {
                return ServiceResult<LivrosDTO>.Error("Autor não encontrado");
            }

            var mapearAutor = _mapper.Map<Livros>(livros);
            var autorAtualizado = await _repository.AtualizarLivrosPorID(id, mapearAutor);

            if (!autorAtualizado.IsSuccess)
            {
                return ServiceResult<LivrosDTO>.Error(autorAtualizado.ErrorMessage);
            }

            var autorRetornado = _mapper.Map<LivrosDTO>(autorAtualizado.Data);
            return ServiceResult<LivrosDTO>.Success(autorRetornado);
        }
        catch (Exception ex)
        {
            return ServiceResult<LivrosDTO>.Error(ex.Message);
        }
    }

    public async Task<ServiceResult<LivrosDTO>> ComprarLivroPorID(long id, int quantidade)
    {
        var livro = await _repository.ProcuraLivrosPorIDRepository(id);
        if (!livro.IsSuccess || livro.Data == null || livro.Data.Quantidade < quantidade)
        {
            return ServiceResult<LivrosDTO>.Error("Estoque insuficiente ou livro não encontrado.");
        }

        livro.Data.Quantidade -= quantidade;
        var EntidadeRepository = await _repository.ComprarLivroPorID(id, quantidade);
        var Mapper = _mapper.Map<LivrosDTO>(EntidadeRepository);
        return ServiceResult<LivrosDTO>.Success(Mapper);
    }

    public async Task<ServiceResult<LivrosDTO>> ComprarLivroPorTitulo(string titulo, int quantidade)
    {
        var livro = await _repository.ProcuraLivrosPorTitulo(titulo);
        if (!livro.IsSuccess || livro.Data.Quantidade < quantidade)
        {
            return ServiceResult<LivrosDTO>.Error("Estoque insuficiente ou livro não encontrado.");
        }
        livro.Data.Quantidade -= quantidade;
        await _repository.AtualizarLivrosPorID(livro.Data.Id, livro.Data);

        return ServiceResult<LivrosDTO>.Success(_mapper.Map<LivrosDTO>(livro.Data));
    }

    public async Task<ServiceResult<List<LivrosDTO>>> BuscarLivrosPorTituloOuCategoriaService(string titulo, CategoriaDosLivrosEnum? categoria)
    {
        var livrosResult = await _repository.BuscarLivrosPorTituloOuCategoria(titulo, categoria);

        if (!livrosResult.IsSuccess)
        {
            return ServiceResult<List<LivrosDTO>>.Error(livrosResult.ErrorMessage);
        }

        var livrosDTO = _mapper.Map<List<LivrosDTO>>(livrosResult.Data);
        return ServiceResult<List<LivrosDTO>>.Success(livrosDTO);
    }

    public async Task<ServiceResult<List<LivrosDTO>>> BuscarLivrosPorAutorService(long autorId)
    {
        var livros = await _repository.BuscarLivrosPorAutor(autorId);
        return ServiceResult<List<LivrosDTO>>.Success(_mapper.Map<List<LivrosDTO>>(livros));
    }
}
