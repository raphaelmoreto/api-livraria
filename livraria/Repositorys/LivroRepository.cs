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

        public async Task<bool> InserirLivro(CadastrarLivroDto livro)
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
    }
}
