using BookStore.Domain.Entities.Model;
using BookStore.Domain.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BookStore.Application.Interface;

public interface IUsuarioResponseService
{
    public Task<ServiceResult<UsuarioResponse>> ProcuraUsuarioPorIDService(long id);
    public Task<ServiceResult<IPagedList<UsuarioResponse>>> MostrarTodosUsuariosService(int NumeroDaPagina, int TamanhoDaPagina);
    public Task<ServiceResult<UsuarioResponse>> ProcurarUsuarioPorNomeService(string procurarUsuarioPorNome);
}
