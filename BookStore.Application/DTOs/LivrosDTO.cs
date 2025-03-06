using BookStore.Application.Converter;
using BookStore.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Model;

public class LivrosDTO
{
    public long Id { get; set; }
    public string Titulo { get; set; }
    public long AutorID { get; set; }
    public CategoriaDosLivrosEnum Categoria { get; set; }
    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime DataDePublicacao { get; set; }
    public decimal Price { get; set; }
    public int Quantidade { get; set; }
}
