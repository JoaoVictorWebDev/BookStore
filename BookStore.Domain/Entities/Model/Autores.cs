using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities.Model;

public class Autores
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(255)]
    public string Nome { get; set; }
    [Required]
    public DateTime DataNascimento { get; set; }
    [Required]
    [MaxLength(180)]
    public string Pais { get; set; }
    //public ICollection<Livros> Livros { get; set; } = new HashSet<Livros>();
}
