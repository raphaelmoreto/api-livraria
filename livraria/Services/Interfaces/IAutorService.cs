using Dtos.Autor;
using Services;

namespace Service.InterfaceAutor
{
    public interface IAutorService
    {
        //Task<Response<AtualizarAutorDto>> AtualizarAutor(AtualizarAutorDto autorNome, int idAutor);

        Task<Response<CadastrarAutorDto>> CadastrarAutor(CadastrarAutorDto autorNome);

        //Task<Response<bool>> ExcluirAutor(int idAutor);

        //Task<Response<ListarAutorPorIdDto>> ObterAutorPorId(int idAutor);

        //Task<Response<IEnumerable<ListarAutoresDto>>> ObterTodosAutores();
    }
}
