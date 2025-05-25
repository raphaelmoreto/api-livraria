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
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return false;
            }
        }
    }
}
