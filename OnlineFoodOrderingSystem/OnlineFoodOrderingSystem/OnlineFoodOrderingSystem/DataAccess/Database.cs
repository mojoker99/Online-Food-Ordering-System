using System.Configuration;
using System.Data.SqlClient;

namespace OnlineFoodOrderingSystem.DataAccess
{
    public static class Database
    {
        public static SqlConnection GetConnection()
        {
            string cs = ConfigurationManager.ConnectionStrings["OnlineFoodOrderingDB"].ConnectionString;
            return new SqlConnection(cs);
        }
    }
}
