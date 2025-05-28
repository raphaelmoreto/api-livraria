using Models;

namespace Interfaces
{
    public interface IAutorRepository
    {
        Task<bool> InsertAutorAsync(Autor autor);

        Task<IEnumerable<Autor>> GetAutoresAsync();

        Task<Autor?> GetPorIdAsync(int id);

        Task<bool> PutAutorAsync(Autor autor, int idAutor);

        Task<bool> DeleteAutorAsync(int idAutor);
    }
}
