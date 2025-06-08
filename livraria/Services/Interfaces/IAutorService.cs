using Models;
using Dtos.Autor;

namespace Service.InterfaceAutor
{
    public interface IAutorService
    {
        Task<Response<CadastrarAutorDto>> CadastrarAutor(CadastrarAutorDto autorNome);

        Task<Response<IEnumerable<ListarAutoresDto>>> ObterTodosAutores();

        Task<Response<ListarAutorPorIdDto>> ObterAutorPorId(int idAutor);

        Task<Response<AtualizarAutorDto>> AtualizarAutor(AtualizarAutorDto autorNome, int idAutor);

        Task<Response<bool>> ExcluirAutor(int idAutor);
    }
}
