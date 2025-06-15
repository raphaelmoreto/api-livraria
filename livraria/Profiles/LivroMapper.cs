using AutoMapper;
using Dtos.Autor;
using Models;

namespace Profiles
{
    public class LivroMapper : Profile
    {
        public LivroMapper()
        {
            CreateMap<Livro, CadastrarAutorDto>();
        }
    }
}
