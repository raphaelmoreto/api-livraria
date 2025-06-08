using System.Text;
using Dapper;
using Database;
using Models;
using Repository.InterfaceAutor;

namespace Repositorys
{
    public class AutorRepository : IAutorRepository
    {
        private readonly DatabaseConnection _dbConnection;

        public AutorRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> AtualizarAutor(string autorNome, int idAutor)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletarAutor(int idAutor)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE autor");
                sb.AppendLine("SET status_autor = 0");
                sb.AppendLine("WHERE id = @idAutor");

                var linhasAfetadas = await connection.ExecuteAsync(sb.ToString(), new { idAutor });
                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InserirAutor(string nomeAutor)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("INSERT INTO autor (nome)");
                sb.AppendLine("                  VALUES (@nomeAutor)");

                var linhasAfetadas = await connection.ExecuteAsync(sb.ToString(), new { nomeAutor = nomeAutor.ToUpper() });
                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Autor>> SelecionarAutores()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Autor?> SelecionarAutorPorId(int idAutor)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SelecionarAutorPorNome(string autorNome)
        {
            try
            {
                using var connection = _dbConnection.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT COUNT(nome)");
                sb.AppendLine("FROM autor");
                sb.AppendLine("WHERE nome = @autorNome");

                var retorno = await connection.QueryFirstOrDefaultAsync<int>(sb.ToString(), new { autorNome = autorNome.ToUpper() });
                return retorno > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
