using BookStore.Domain.Entities.Model;
using BookStore.Domain.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BookStore.Application.Interface;

public interface IUsuarioRequestService
{
    public Task<ServiceResult<UsuarioRequest>> AdicionarUsuarioService(UsuarioRequest usuarios, string role);
    public Task<ServiceResult<UsuarioRequest>> RemoveUsuarioPorIDService(long id, UsuarioRequest request,string role);
    public Task<ServiceResult<UsuarioRequest>> AtualizaUsuarioPorIDService(long id, UsuarioRequest usuarios, string role);
}
