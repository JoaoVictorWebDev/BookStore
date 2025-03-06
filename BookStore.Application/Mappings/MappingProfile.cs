using AutoMapper;
using BookStore.Domain.Entities.Model;
using BookStore.Domain.Structs;

namespace BookStore.Application.Mappings;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Autores, AutoresDTO>().ReverseMap();
        CreateMap<Livros, LivrosDTO>().ReverseMap();
        CreateMap<Usuario, UsuarioRequest>().ReverseMap();
        CreateMap<Usuario, UsuarioResponse>().ReverseMap();
    }
}
