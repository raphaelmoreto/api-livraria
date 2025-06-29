using Dtos.Autor;
using Services;

namespace Service.InterfaceAutor
{
    public interface IAutorService
    {
        Task<Response<AtualizarAutorDto>> AtualizarAutor(AtualizarAutorDto autorNomeDTO, int idAutor);

        Task<Response<CadastrarAutorDto>> CadastrarAutor(CadastrarAutorDto autorNomeDTO);

        //Task<Response<bool>> ExcluirAutor(int idAutor);

        //Task<Response<ListarAutorPorIdDto>> ObterAutorPorId(int idAutor);

        //Task<Response<IEnumerable<ListarAutoresDto>>> ObterTodosAutores();
    }
}
