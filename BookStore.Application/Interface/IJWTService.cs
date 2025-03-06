using BookStore.Domain.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interface;

public interface IJWTService
{
    public Task<UsuarioResponse?> Authenticate(UsuarioRequest request);
}
