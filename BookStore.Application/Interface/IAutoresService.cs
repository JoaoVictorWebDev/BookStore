using BookStore.Domain.Entities.Model;
using BookStore.Domain.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BookStore.Application.Interface;

public interface IAutoresService
{
    public Task<ServiceResult<AutoresDTO>> AdicionarAutorRepositoryService(AutoresDTO autores);
    public Task<ServiceResult<AutoresDTO>> RemoveAutorPorIDService(long id);
    public Task<ServiceResult<AutoresDTO>> ProcuraAutorPorIDService(long id);
    public Task<ServiceResult<IPagedList<AutoresDTO>>> MostrarTodosOsAutoresService(int NumeroDaPagina, int TamanhoDaPagina);
    public Task<ServiceResult<AutoresDTO>> AtualizaAutorPorIDService(long id, AutoresDTO autores);
}
