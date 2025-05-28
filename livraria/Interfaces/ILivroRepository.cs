using Models;

namespace Interfaces
{
    public interface ILivroRepository
    {
        Task<bool> InsertLivroAsync(Livro livro);

        Task<IEnumerable<Livro>> GetLivrosAsync();

        Task<IEnumerable<Livro>> GetLivroPorIdAsync(int id);

        Task<bool> PutLivroAsync(Livro livro, int id);

        Task<bool> DeleteLivro(int id);
    }
}
