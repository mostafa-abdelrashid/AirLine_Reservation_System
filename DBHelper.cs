using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AirLine_Reservation_System
{
    public class DBHelper
    {
        private readonly string connectionString = "Data Source=(local);Initial Catalog=Airline Reservation System;Integrated Security=SSPI;TrustServerCertificate=True;";
        private SqlConnection connection;
        public DBHelper()
        {
            connection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            connection.Open();
        }
        public void CloseConnection()
        {
            connection.Close();
        }
        public DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                OpenConnection();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                CloseConnection();
            }
            return dt;
        }
        public int ExecuteNonQuery(string query)
        {
            int result = 0;
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                OpenConnection();
                result = cmd.ExecuteNonQuery();
                CloseConnection();
            }
            return result;
        }
    }
}
