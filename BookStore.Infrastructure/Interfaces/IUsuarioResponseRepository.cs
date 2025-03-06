using BookStore.Domain.Entities.Model;
using BookStore.Domain.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BookStore.Infrastructure.Interfaces;

public interface IUsuarioResponseRepository
{
    public Task<ServiceResult<Usuario>> ProcuraUsuarioPorIDRepository(long id);
    public Task<ServiceResult<IPagedList<Usuario>>> MostrarTodosUsuariosRepository(int NumeroDaPagina, int TamanhoDaPagina);
    public Task<ServiceResult<Usuario>> ProcurarUsuarioPorNomeRepository(string procurarUsuarioPorNome);

}
