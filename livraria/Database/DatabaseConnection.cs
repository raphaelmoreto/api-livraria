using System.Data;
using Microsoft.Data.SqlClient;
using Database.Interface;

namespace Database
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly IConfiguration _configuration; //GUARDA A REFERÊNCIA À CONFIGURAÇÕES DO "appsettings.json"

        private readonly string _connectionString; //ARMAZENA A STRING DE CONEXÃO LIDA A PARTIR DA CONFIGURAÇÃO CITADA ACIMA

        public DatabaseConnection(IConfiguration configuration)
        {
            _configuration = configuration;

            // O "!" NO FINAL DA LINHA GARANTE QUE A CONEXÃO COM O BANCO NÃO SERÁ NULA
            _connectionString = _configuration.GetConnectionString("DefaultConnection")!;
        }

        //O TIPO DE RETORNO "IDbConnection" É UMA INTERFACE COMUM PARA CONEXÕES COM BANCO DE DADOS
        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
