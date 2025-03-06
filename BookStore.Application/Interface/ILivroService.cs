using BookStore.Domain.Entities.Model;
using BookStore.Domain.Enum;
using BookStore.Domain.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BookStore.Application.Interface;

public interface ILivroService
{
    public Task<ServiceResult<LivrosDTO>> AdicionarLivrosService(LivrosDTO livros);
    public Task<ServiceResult<LivrosDTO>> RemoveLivrosService(long id);
    public Task<ServiceResult<LivrosDTO>> ProcuraLivrosPorIDService(long id);
    public Task<ServiceResult<IPagedList<LivrosDTO>>> MostrarTodosOsService(int numeroDaPagina, int tamanhoDaPagina);
    public Task<ServiceResult<LivrosDTO>> AtualizarLivrosPorID(long id, LivrosDTO livros);
    public Task<ServiceResult<LivrosDTO>> ComprarLivroPorID(long id, int quantidade);
    public Task<ServiceResult<LivrosDTO>> ComprarLivroPorTitulo(string titulo, int quantidade);
    public Task<ServiceResult<List<LivrosDTO>>> BuscarLivrosPorTituloOuCategoriaService(string titulo, CategoriaDosLivrosEnum? categoria);
    public Task<ServiceResult<List<LivrosDTO>>> BuscarLivrosPorAutorService(long autorId);

}
