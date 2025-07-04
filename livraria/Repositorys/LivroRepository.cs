using Models;
using Dtos.Livro;
using Database.Interface;
using Repository.InterfaceLivro;
using System.Text;
using Dapper;

namespace Repositorys
{
    public class LivroRepository : ILivroRepository
    {
        private readonly IDatabaseConnection _dbConnection;
        public LivroRepository(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> AtualizarLivro(Livro livro)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE livro");
            sb.AppendLine("SET titulo = @titulo,");
            sb.AppendLine("      ano_publicacao = @anoPublicacao,");
            sb.AppendLine("      fk_autor = @idAutor");
            sb.AppendLine("WHERE id = @id;");

            var parameters = new
            {
                id = livro.Id,
                titulo = livro.Titulo.ToUpper(),
                anoPublicacao = livro.AnoPublicacao,
                idAutor = livro.IdAutor
            };

            var linhasAfetadas = await connection.ExecuteAsync(sb.ToString(), parameters);
            return linhasAfetadas > 0;
        }

        public async Task<bool> InserirLivro(Livro livro)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO livro (titulo, ano_publicacao, fk_autor)");
            sb.AppendLine("             VALUES (@titulo, @anoPublicacao, @idAutor)");

            var parameters = new
            {
                titulo = livro.Titulo.ToUpper(),
                anoPublicacao = livro.AnoPublicacao,
                idAutor = livro.IdAutor
            };

            var linhasAfetadas = await connection.ExecuteAsync(sb.ToString(), parameters);
            return linhasAfetadas > 0;
        }

        public Task<IEnumerable<ListarLivrosPorAutor>> SelecionarLivroPorAutor(string nomeAutor)
        {
            throw new NotImplementedException();
        }

        public async Task<ListarLivroPorNome?> SelecionarLivroPorNome(string livroNome)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT l.titulo,");
            sb.AppendLine("           l.ano_publicacao AS 'anoPublicacao',");
            sb.AppendLine("           CASE");
            sb.AppendLine("                  WHEN a.nome IS NULL OR a.nome = '' THEN 'ANÔNIMO'");
            sb.AppendLine("                  ELSE a.nome");
            sb.AppendLine("           END AS 'autor'");
            sb.AppendLine("FROM livro l");
            sb.AppendLine("LEFT JOIN autor a ON l.fk_autor = a.id");
            sb.AppendLine("WHERE l.titulo = @livroNome;");

            var livro = await connection.QueryFirstOrDefaultAsync<ListarLivroPorNome?>(sb.ToString(), new { livroNome = livroNome.ToUpper() });
            return livro;
        }

        public async Task<IEnumerable<ListarLivrosDto>> SelecionarTodosLivros()
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT l.titulo,");
            sb.AppendLine("           l.ano_publicacao AS 'anoPublicacao',");
            sb.AppendLine("           CASE");
            sb.AppendLine("                  WHEN a.nome IS NULL OR a.nome = '' THEN 'ANÔNIMO'");
            sb.AppendLine("                  ELSE a.nome");
            sb.AppendLine("           END AS 'autor'");
            sb.AppendLine("FROM livro l");
            sb.AppendLine("LEFT JOIN autor a ON l.fk_autor = a.id");
            sb.AppendLine("ORDER BY l.titulo");

            var livros = await connection.QueryAsync<ListarLivrosDto>(sb.ToString());
            return livros;
        }

        public async Task<bool> VerificarSeExisteLivroPorNome(string nomeLivro)
        {
            using var connection = _dbConnection.GetConnection();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT COUNT(*)");
            sb.AppendLine("FROM livro");
            sb.AppendLine("WHERE UPPER(titulo) = @nomeLivro;");

            var retorno = await connection.ExecuteScalarAsync<int>(sb.ToString(), new { nomeLivro = nomeLivro.ToUpper() });
            return retorno > 0;
        }
    }
}
