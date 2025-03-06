using BookStore.Domain.Entities.Model;
using BookStore.Domain.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BookStore.Infrastructure.Interfaces;

public interface IUsuarioRepositoryRequest
{
    public Task<ServiceResult<Usuario>> AdicionarUsuarioRepository(Usuario usuarios, string role);
    public Task<ServiceResult<Usuario>> RemoveUsuarioPorIDRepository(long id, string role);
    public Task<ServiceResult<Usuario>> AtualizaUsuarioPorIDRepository(long id, Usuario usuario, string role);

}
