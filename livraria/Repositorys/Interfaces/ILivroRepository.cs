using Dtos.Livro;

namespace Repository.InterfaceLivro
{
    public interface ILivroRepository
    {
        Task<bool> InserirLivro(CadastrarLivroDto livro);

        Task<IEnumerable<ListarLivrosDto>> SelecionarTodosLivros();
    }
}
