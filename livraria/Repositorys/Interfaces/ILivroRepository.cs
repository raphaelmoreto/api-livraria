using Dtos.Livro;

namespace Repository.InterfaceLivro
{
    public interface ILivroRepository
    {
        Task<bool> AtualizarLivro(AtualizarLivroDto livro, int idLivro);

        Task<bool> InserirLivro(CadastrarLivroDto livro);

        Task<IEnumerable<ListarLivrosDto>> SelecionarTodosLivros();

        Task<bool> VerificarSeExisteLivroPorNome(string nomeLivro);

        Task<ListarLivroPorNome?> SelecionarLivroPorNome(string livroNome);

        Task<IEnumerable<ListarLivrosPorAutor>> SelecionarLivroPorAutor(string nomeAutor);
    }
}
