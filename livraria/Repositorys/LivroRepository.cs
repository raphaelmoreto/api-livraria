using System.Text;
using Dapper;
using Models;
using Database;
using Interfaces;

namespace Repositorys
{
    public class LivroRepository : ILivroRepository
    {
        private readonly DatabaseConnection _dbConnection;

        public LivroRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> InsertLivroAsync(Livro livro)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("INSERT INTO livro (titulo, ano_publicacao, fk_autor)");
                sb.AppendLine("              VALUES (@titulo, @anoPublicacao, @idAutor)");

                var parameters = new
                {
                    titulo = livro.Titulo.ToUpper(),
                    anoPublicacao = livro.AnoPublicacao,
                    idAutor = livro.IdAutor
                };

                var linhasAfetadas = await connection.ExecuteAsync(sb.ToString(), parameters);
                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Livro>> GetLivrosAsync()
        {
            try
            {
                using var connection = _dbConnection.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT *");
                sb.AppendLine("FROM livro");

                var listaLivros = await connection.QueryAsync<Livro>(sb.ToString());
                return listaLivros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<IEnumerable<Livro>> GetLivroPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutLivroAsync(Livro livro, int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLivro(int id)
        {
            throw new NotImplementedException();
        }
    }
}
