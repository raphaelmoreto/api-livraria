using Models;
using Dtos.Autor;

namespace Repository.InterfaceAutor
{
    public interface IAutorRepository
    {
        Task<bool> AtualizarAutor(AtualizarAutorDto autorNomeDTO, int idAutor);

        Task<bool> DeletarAutor(int idAutor);

        Task<bool> InserirAutor(CadastrarAutorDto nomeAutor);

        Task<IEnumerable<Autor>> SelecionarAutores();

        Task<Autor?> SelecionarAutorPorNome(string autorNome);

        Task<bool> VerificarAutorPorNome(string autorNome);
    }
}
