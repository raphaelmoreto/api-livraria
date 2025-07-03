using Models;
using Dtos.Livro;

namespace Repository.InterfaceLivro
{
    public interface ILivroRepository
    {
        Task<bool> AtualizarLivro(Livro livro);

        Task<bool> InserirLivro(Livro livro);

        Task<IEnumerable<ListarLivrosPorAutor>> SelecionarLivroPorAutor(string nomeAutor);

        Task<ListarLivroPorNome?> SelecionarLivroPorNome(string livroNome);

        Task<IEnumerable<ListarLivrosDto>> SelecionarTodosLivros();

        Task<bool> VerificarSeExisteLivroPorNome(string nomeLivro);
    }
}
