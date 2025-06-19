using System.Text;
using Dapper;
using Database.Interface;
using Models;
using Repository.InterfaceAutor;

namespace Repositorys
{
    public class AutorRepository : IAutorRepository
    {
        private readonly IDatabaseConnection _dbConnection;

        public AutorRepository(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> AtualizarAutor(string autorNome, int idAutor)
        { 
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE autor");
            sb.AppendLine("SET nome = @autorNome");
            sb.AppendLine("WHERE id = @idAutor");

            var parameters = new
            {
                idAutor,
                autorNome = autorNome.ToUpper()
            };

            var autorAtualizado = await connection.ExecuteAsync(sb.ToString(), parameters);
            return autorAtualizado > 0;
        }

        public async Task<bool> DeletarAutor(int idAutor)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE autor");
            sb.AppendLine("SET status_autor = 0");
            sb.AppendLine("WHERE id = @idAutor");

            var linhasAfetadas = await connection.ExecuteAsync(sb.ToString(), new { idAutor });
            return linhasAfetadas > 0;
        }

        public async Task<bool> InserirAutor(string nomeAutor)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO autor (nome)");
            sb.AppendLine("               VALUES (@nomeAutor)");

            var linhasAfetadas = await connection.ExecuteAsync(sb.ToString(), new { nomeAutor = nomeAutor.ToUpper() });
            return linhasAfetadas > 0;
        }

        public async Task<IEnumerable<Autor>> SelecionarAutores()
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT id,");
            sb.AppendLine("           nome,");
            sb.AppendLine("           status_autor AS 'statusAutor'");
            sb.AppendLine("FROM autor");

            var autores = await connection.QueryAsync<Autor>(sb.ToString());
            return autores;
        }

        public async Task<Autor?> SelecionarAutorPorId(int idAutor)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT id,");
            sb.AppendLine("           nome,");
            sb.AppendLine("           status_autor AS statusAutor");
            sb.AppendLine("FROM autor");
            sb.AppendLine("WHERE id = @idAutor");

            var autor = await connection.QueryFirstOrDefaultAsync<Autor>(sb.ToString(), new { idAutor });
            return autor;
        }

        public async Task<bool> SelecionarAutorPorNome(string autorNome)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT COUNT(nome)");
            sb.AppendLine("FROM autor");
            sb.AppendLine("WHERE nome = @autorNome");

            var retorno = await connection.QueryFirstOrDefaultAsync<int>(sb.ToString(), new { autorNome = autorNome.ToUpper() });
            return retorno >= 0;
        }
    }
}
