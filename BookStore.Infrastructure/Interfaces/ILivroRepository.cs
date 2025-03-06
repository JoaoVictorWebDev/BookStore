using BookStore.Domain.Entities.Model;
using BookStore.Domain.Enum;
using BookStore.Domain.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BookStore.Infrastructure.Interfaces;

public interface ILivroRepository
{
    public Task<ServiceResult<Livros>> AdicionarLivrosRepository(Livros livros);
    public Task<ServiceResult<Livros>> RemoveLivrosRepository(long id);
    public Task<ServiceResult<Livros>> ProcuraLivrosPorIDRepository(long id);
    public Task<ServiceResult<IPagedList<Livros>>> MostrarTodosOsLivros(int NumeroDaPagina, int TamanhoDaPagina);
    public Task<ServiceResult<Livros>> AtualizarLivrosPorID(long id, Livros livros);
    public Task<ServiceResult<Livros>> ComprarLivroPorID(long id, int quantidade);
    public Task<ServiceResult<Livros>> ComprarLivroPorTitulo(string Titulo, int quantidade);
    public Task<ServiceResult<List<Livros>>> BuscarLivrosPorTituloOuCategoria(string titulo, CategoriaDosLivrosEnum? categoriaId);
    public Task<ServiceResult<List<Livros>>> BuscarLivrosPorAutor(long autorId);
    Task<ServiceResult<Livros>> ProcuraLivrosPorTitulo(string titulo);

}
