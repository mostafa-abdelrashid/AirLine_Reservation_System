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
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }
        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        public DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dt.Columns.Add(reader.GetName(i));
                        }
                        while (reader.Read())
                        {
                            DataRow row = dt.NewRow();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[i] = reader[i];
                            }
                            dt.Rows.Add(row);
                        }
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }
        public int ExecuteNonQuery(string query)
        {
            int result = 0;
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    result = cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                CloseConnection();
            }
            return result;
        }
    }
}
