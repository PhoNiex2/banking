using System.Configuration;
using System.Data.SqlClient;

namespace banking.Data
{
    public class DatabaseHelper
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}