using AutoMapper;
using Dtos.Autor;
using Models;

namespace Profiles
{
    public class AutorMapper : Profile
    {
        public AutorMapper()
        {
            CreateMap<Autor, AtualizarAutorDto>();
            CreateMap<Autor, CadastrarAutorDto>();
            CreateMap<Autor, ListarAutoresDto>();
            CreateMap<Autor, ListarAutorPorIdDto>();
        }
    }
}
