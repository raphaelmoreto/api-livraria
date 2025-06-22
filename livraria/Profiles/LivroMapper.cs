using AutoMapper;
using Dtos.Livro;
using Models;

namespace Profiles
{
    public class LivroMapper : Profile
    {
        public LivroMapper()
        {
            CreateMap<Livro, CadastrarLivroDto>();
            CreateMap<Livro, ListarLivrosDto>();
            CreateMap<Livro, AtualizarLivroDto>();
            CreateMap<Livro, ListarLivroPorNome>();
        }
    }
}
