using Dtos.Livro;
using Services;

namespace Service.InterfaceLivro
{
    public interface ILivroService
    {
        Task<Response<AtualizarLivroDto>> AtualizarLivro(AtualizarLivroDto livro, int idLivro);

        Task<Response<IEnumerable<ListarLivrosPorAutor>>> BuscarLivrosPorAutor(string nomeAutor);

        Task<Response<ListarLivroPorNome>> BuscarLivroPorNome(string livroNome);

        Task<Response<IEnumerable<ListarLivrosDto>>> BuscarTodosLivros();

        Task<Response<CadastrarLivroDto>> CadastrarLivro(CadastrarLivroDto livroDTO);
    }
}
