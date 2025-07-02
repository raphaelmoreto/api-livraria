using Models;
using Dtos.Autor;

namespace Repository.InterfaceAutor
{
    public interface IAutorRepository
    {
        Task<bool> AtualizarAutor(Autor autor, int idAutor);

        Task<bool> DeletarAutor(int idAutor);

        Task<bool> InserirAutor(Autor autor);

        Task<IEnumerable<ListarAutoresDto>> SelecionarAutores();

        Task<ListarAutorPorNomeDto?> SelecionarAutorPorNome(string autorNome);

        Task<bool> VerificarAutorPorNome(string autorNome);
    }
}
