using System.Data.SqlClient;

namespace DataBaseConnection
{
    public class Connection
    {
        private static readonly SqlConnection conn = new SqlConnection();

        public Connection()
        {
            conn.ConnectionString = @"Data Source=DESKTOP-61L2M0C\SQLEXPRESS;integrated security=SSPI;initial Catalog=TesteV3";
            conn.Open();
        }

        public SqlConnection Connect()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            return conn;
        }

        public SqlConnection Disconnect()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return conn;
        }
    }
}