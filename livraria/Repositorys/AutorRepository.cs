using System.Text;
using Dapper;
using Database;
using Models;

namespace Repositorys
{
    public class AutorRepository
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
                sb.Append("INSERT INTO autor (nome)");
                sb.Append("                  VALUES (@nome)");

                var linhasAfetadas = await connection.ExecuteAsync(sb.ToString(), new { nome = autor.Nome.ToUpper()});
                return linhasAfetadas > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Autor>> GetAutoresAsync()
        {
            try
            {
                using var connection = _dbConnection.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * ");
                sb.Append("FROM autor");

                var listaAutores = await connection.QueryAsync<Autor>(sb.ToString());
                return listaAutores;
            }
            catch (Exception e)
            {
                throw new Exception("ERRO! " + e.Message);
            }
        }

        public async Task<Autor?> GetPorIdAsync(int id)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * ");
                sb.Append("FROM autor ");
                sb.Append("WHERE id = @id");

                var autor = await connection.QueryFirstOrDefaultAsync<Autor>(sb.ToString(), new { id });
                return autor;
            }
            catch (Exception e)
            {
                throw new Exception("ERRO! " + e.Message);
            }
        }

        public async Task<bool> PutAutorAsync(Autor autor, int idAutor)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE autor ");
                sb.Append("SET nome = @nome ");
                sb.Append("WHERE id = @id");

                var parameters = new
                {
                    id = idAutor,
                    nome = autor.Nome.ToUpper()
                };

                var autorAtualizado = await connection.ExecuteAsync(sb.ToString(), parameters);
                return autorAtualizado > 0;
            }
            catch (Exception e)
            {
                throw new Exception("ERRO! " + e.Message);
            }
        }

        public async Task<bool> DeleteAutorAsync(int idAutor)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE autor ");
                sb.Append("SET status_autor = 0 ");
                sb.Append("WHERE id = @idAutor");

                var linhasAfetadas = await connection.ExecuteAsync(sb.ToString(), new { idAutor });
                return linhasAfetadas > 0;
            }
            catch (Exception e)
            {
                throw new Exception("ERRO!" + e.Message);
            }
        }
    }
}
