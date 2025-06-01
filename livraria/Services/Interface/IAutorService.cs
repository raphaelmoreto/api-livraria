using Models;

namespace Service.InterfaceAutor
{
    public interface IAutorService
    {
        Task<bool> CadastrarAutor(string nomeAutor);

        Task<IEnumerable<Autor>> ObterTodosAutores();

        Task<Autor?> ObterAutorPorId(int idAutor);

        Task<bool> AtualizarAutor(string nomeAutor, int idAutor);

        Task<bool> ExcluirAutor(int idAutor);
    }
}
