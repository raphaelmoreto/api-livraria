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

                StringBuilder sb.
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
