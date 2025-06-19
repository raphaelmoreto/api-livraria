using Models;
using Dtos.Livro;

namespace Service.InterfaceLivro
{
    public interface ILivroService
    {
        Task<Response<CadastrarLivroDto>> CadastrarLivro(CadastrarLivroDto livro);

        Task<Response<IEnumerable<ListarLivrosDto>>> BuscarTodosLivros();
    }
}
