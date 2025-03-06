using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Model;

public class UsuarioRequest
{
    public string? NomeDeUsuario { get; set; }
    public string? Email { get; set; }
    public string Senha { get; set; }
    public string Cargo { get; set; }

}