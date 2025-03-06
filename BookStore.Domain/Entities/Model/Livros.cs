using BookStore.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Model;

public class Livros
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(255)]
    public string ? Titulo { get; set; }
    [ForeignKey(nameof(Autor))]
    public long AutorID { get; set; }
    public Autores ? Autor { get; set; }
    public CategoriaDosLivrosEnum Categoria { get; set; }
    [Required]
    public DateTime DataDePublicacao { get; set; } = DateTime.UtcNow;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int Quantidade { get; set; }
}
