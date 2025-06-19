using Models;
using Dtos.Livro;

namespace Service.InterfaceLivro
{
    public interface ILivroService
    {
        Task<Response<AtualizarLivroDto>> AtualizarLivro(AtualizarLivroDto livro, int idLivro);

        Task<Response<CadastrarLivroDto>> CadastrarLivro(CadastrarLivroDto livro);

        Task<Response<IEnumerable<ListarLivrosDto>>> BuscarTodosLivros();

        Task<Response<ListarLivroPorNome>> BuscarLivroPorNome(string livroNome);

        Task<Response<IEnumerable<ListarLivrosPorAutor>>> BuscarLivrosPorAutor(string nomeAutor);
    }
}
