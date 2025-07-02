using System.Text;
using Dapper;
using Database.Interface;
using Dtos.Autor;
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

        public async Task<bool> AtualizarAutor(Autor autor)
        { 
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE autor");
            sb.AppendLine("SET nome = @autorNome");
            sb.AppendLine("WHERE id = @idAutor");

            var parameters = new
            {
                idAutor = autor.Id,
                autorNome = autor.Nome.ToUpper()
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

        public async Task<bool> InserirAutor(Autor autor)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO autor (nome)");
            sb.AppendLine("               VALUES (@autor)");

            var linhasAfetadas = await connection.ExecuteAsync(sb.ToString(), new { autor = autor.Nome.ToUpper() });
            return linhasAfetadas > 0;
        }

        public async Task<IEnumerable<ListarAutoresDto>> SelecionarAutores()
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT nome,");
            sb.AppendLine("           CASE status_autor");
            sb.AppendLine("                   WHEN 1 THEN 'ATIVO'");
            sb.AppendLine("                   WHEN 0 THEN 'INATIVO'");
            sb.AppendLine("           END AS 'statusAutor'");
            sb.AppendLine("FROM autor");

            var autores = await connection.QueryAsync<ListarAutoresDto>(sb.ToString());
            return autores;
        }

        public async Task<ListarAutorPorNomeDto?> SelecionarAutorPorNome(string autorNome)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT nome,");
            sb.AppendLine("           CASE status_autor");
            sb.AppendLine("                   WHEN 1 THEN 'ATIVO'");
            sb.AppendLine("                   WHEN 0 THEN 'INATIVO'");
            sb.AppendLine("           END AS 'statusAutor'");
            sb.AppendLine("FROM autor");
            sb.AppendLine("WHERE nome = @autorNome");

            var autor = await connection.QueryFirstOrDefaultAsync<ListarAutorPorNomeDto>(sb.ToString(), new { autorNome });
            return autor;
        }

        public async Task<bool> VerificarAutorPorNome(string autorNome)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT COUNT(*)");
            sb.AppendLine("FROM autor");
            sb.AppendLine("WHERE UPPER(nome) = @autorNome");

            var retorno = await connection.ExecuteScalarAsync<int>(sb.ToString(), new { autorNome = autorNome.ToUpper() });
            return retorno > 0;
        }
    }
}
