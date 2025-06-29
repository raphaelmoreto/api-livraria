using Models;
using Dtos.Autor;

namespace Repository.InterfaceAutor
{
    public interface IAutorRepository
    {
        Task<bool> AtualizarAutor(string nomeAutor, int idAutor);

        Task<bool> DeletarAutor(int idAutor);

        Task<bool> InserirAutor(string nomeAutor);

        Task<IEnumerable<Autor>> SelecionarAutores();

        Task<ListarAutorPorNomeDto?> SelecionarAutorPorNome(string autorNome);

        Task<bool> VerificarAutorPorNome(string autorNome);
    }
}
