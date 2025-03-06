using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Model;

public class Usuario
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(255)]
    [Column("NomeDeUsuario")]
    public string NomeDeUsuario { get; set; }
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; }
    [Required]
    [MaxLength(255)]
    public string Senha { get; set; }
    public string Cargo { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public string? TokenDeAcesso { get; set; }
    public int ExpiraEm { get; set; }
}
