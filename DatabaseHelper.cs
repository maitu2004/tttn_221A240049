using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanHang
{
    public static class DatabaseHelper
    {
        // <-- Update: server name provided by you -->
        private static string connectionString = @"Data Source=B402-25\MISASME2019;Initial Catalog=QUANLYBANHANG;Integrated Security=True";

        public static DataTable GetData(string query)
        {
            using (var da = new SqlDataAdapter(query, connectionString))
            {
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static void Execute(string query)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
