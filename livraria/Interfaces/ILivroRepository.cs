using Models;

namespace Interfaces
{
    public interface ILivroRepository
    {
        Task<bool> InsertLivroAsync(Livro livro);

        Task<Livro> GetLivrosAsync();

        Task<Livro> GetLivroPorIdAsync(int id);

        Task<bool> PutLivroAsync(Livro livro, int id);

        Task<bool> DeleteLivro(int id);
    }
}
