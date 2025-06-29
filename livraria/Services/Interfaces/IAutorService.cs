using Dtos.Autor;
using Services;

namespace Service.InterfaceAutor
{
    public interface IAutorService
    {
        Task<Response<AtualizarAutorDto>> AtualizarAutor(AtualizarAutorDto autorNomeDTO, int idAutor);

        Task<Response<CadastrarAutorDto>> CadastrarAutor(CadastrarAutorDto autorNomeDTO);

        //Task<Response<bool>> ExcluirAutor(int idAutor);

        Task<Response<ListarAutorPorNomeDto>> ObterAutorPorNome(string autorNome);

        Task<Response<IEnumerable<ListarAutoresDto>>> ObterTodosAutores();
    }
}
