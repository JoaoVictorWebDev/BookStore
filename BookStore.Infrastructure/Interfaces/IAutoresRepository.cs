using BookStore.Domain.Entities.Model;
using BookStore.Domain.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BookStore.Infrastructure.Interfaces;

public interface IAutoresRepository
{
    public Task <ServiceResult<Autores>> AdicionarAutorRepository(Autores autores);
    public Task <ServiceResult<Autores>> RemoveAutorPorID(long id);
    public Task <ServiceResult<Autores>> ProcuraAutorPorID(long id);
    public Task <ServiceResult<IPagedList<Autores>>> MostrarTodosOsAutores(int NumeroDaPagina, int TamanhoDaPagina);
    public Task <ServiceResult<Autores>> AtualizaAutorPorID(long id, Autores autores);
}
