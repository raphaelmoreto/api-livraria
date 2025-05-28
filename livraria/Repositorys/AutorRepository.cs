using System.Text;
using Dapper;
using Database;
using Models;
using Interfaces;

namespace Repositorys
{
    public class AutorRepository : IAutorRepository
    {
        private readonly DatabaseConnection _dbConnection;

        public AutorRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> InsertAutorAsync(Autor autor)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("INSERT INTO autor (nome)");
                sb.AppendLine("                  VALUES (@nome)");

                var linhasAfetadas = await connection.ExecuteAsync(sb.ToString(), new { nome = autor.Nome.ToUpper()});
                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Autor>> GetAutoresAsync()
        {
            try
            {
                using var connection = _dbConnection.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * ");
                sb.AppendLine("FROM autor");

                var listaAutores = await connection.QueryAsync<Autor>(sb.ToString());
                return listaAutores;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Autor?> GetPorIdAsync(int id)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT * ");
                sb.AppendLine("FROM autor ");
                sb.AppendLine("WHERE id = @id");

                var autor = await connection.QueryFirstOrDefaultAsync<Autor>(sb.ToString(), new { id });
                return autor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> PutAutorAsync(Autor autor, int idAutor)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE autor ");
                sb.AppendLine("SET nome = @nome ");
                sb.AppendLine("WHERE id = @id");

                var parameters = new
                {
                    id = idAutor,
                    nome = autor.Nome.ToUpper()
                };

                var autorAtualizado = await connection.ExecuteAsync(sb.ToString(), parameters);
                return autorAtualizado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAutorAsync(int idAutor)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE autor ");
                sb.AppendLine("SET status_autor = 0 ");
                sb.AppendLine("WHERE id = @idAutor");

                var linhasAfetadas = await connection.ExecuteAsync(sb.ToString(), new { idAutor });
                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
