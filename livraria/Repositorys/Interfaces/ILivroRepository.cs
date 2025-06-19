using Dtos.Livro;

namespace Repository.InterfaceLivro
{
    public interface ILivroRepository
    {
        Task<bool> AtualizarLivro(AtualizarLivroDto atualizarLivro);

        Task<bool> InserirLivro(CadastrarLivroDto livro);

        Task<IEnumerable<ListarLivrosDto>> SelecionarTodosLivros();
    }
}
