using System.Data;

namespace Database.Interface
{
    public interface IDatabaseConnection
    {
        IDbConnection GetConnection();
    }
}
