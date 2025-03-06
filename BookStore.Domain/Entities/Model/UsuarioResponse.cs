using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Model;

public class UsuarioResponse
{

    public long Id { get; set; }
    public string? NomeDeUsuario { get; set; }
    public string? Email { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public string? TokenDeAcesso { get; set; }
    public int ExpiraEm { get; set; }
}
