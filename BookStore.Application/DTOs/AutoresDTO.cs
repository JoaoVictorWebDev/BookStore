using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BookStore.Application.Converter;

namespace BookStore.Domain.Entities.Model;

public class AutoresDTO
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(255)]
    public string Nome { get; set; }
    [Required]
    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime DataNascimento { get;set; }
    [Required]
    [MaxLength(180)]
    public string Pais { get; set; }
}
