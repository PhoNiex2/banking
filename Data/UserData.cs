using System.Data;
using System.Data.SqlClient;

namespace banking.Data
{
    public class UserData
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        public bool Login(string username, string password)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_Login", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                return reader.HasRows;
            }
        }
    }
}