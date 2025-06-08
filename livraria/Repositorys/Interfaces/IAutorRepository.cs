using Models;

namespace Repository.InterfaceAutor
{
    public interface IAutorRepository
    {
        Task<bool> InserirAutor(string nomeAutor);

        Task<IEnumerable<Autor>> SelecionarAutores();

        Task<Autor?> SelecionarAutorPorId(int idAutor);

        Task<bool> SelecionarAutorPorNome(string nomeAutor);

        Task<bool> AtualizarAutor(string nomeAutor, int idAutor);

        Task<bool> DeletarAutor(int idAutor);
    }
}
