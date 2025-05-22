using System.Data;
using System.Data.SqlClient;

namespace Database
{
    public class DatabaseConnection
    {
        private readonly IConfiguration _configuration; //GUARDA A REFERÊNCIA À CONFIGURAÇÕES DO "appsettings.json"

        private readonly string _connectionString; //ARMAZENA A STRING DE CONEXÃO LIDA A PARTIR DA CONFIGURAÇÃO CITADA ACIMA

        public DatabaseConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        //O TIPO DE RETORNO "IDbConnection" É UMA INTERFACE COMUM PARA CONEXÕES COM BANCO DE DADOS
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
