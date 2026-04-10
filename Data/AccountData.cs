using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using banking.Models;

namespace banking.Data
{
    public class AccountData
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();

            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllAccounts", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    accounts.Add(new Account
                    {
                        AccountNumber = (int)reader["AccountNumber"],
                        FirstName = reader["FirstName"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        AddressLine1 = reader["AddressLine1"].ToString(),
                        AddressLine2 = reader["AddressLine2"].ToString(),
                        City = reader["City"].ToString(),
                        County = reader["County"].ToString(),
                        AccountType = reader["AccountType"].ToString(),
                        SortCode = (int)reader["SortCode"],
                        Balance = (decimal)reader["Balance"],
                        OverdraftLimit = (decimal)reader["OverdraftLimit"]
                    });
                }
            }

            return accounts;
        }

        public void DeactivateAccount(int accountNumber)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Accounts SET IsActive = 0 WHERE AccountNumber = @AccountNumber",
                    conn);

                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Account GetAccountByNumber(int accountNumber)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_GetAccountByNumber", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Account
                    {
                        AccountNumber = (int)reader["AccountNumber"],
                        FirstName = reader["FirstName"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        AddressLine1 = reader["AddressLine1"].ToString(),
                        AddressLine2 = reader["AddressLine2"].ToString(),
                        City = reader["City"].ToString(),
                        County = reader["County"].ToString(),
                        AccountType = reader["AccountType"].ToString(),
                        SortCode = (int)reader["SortCode"],
                        Balance = (decimal)reader["Balance"],
                        OverdraftLimit = (decimal)reader["OverdraftLimit"]
                    };
                }
            }

            return null;
        }

        public void AddAccount(Account account)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_AddAccount", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
                cmd.Parameters.AddWithValue("@FirstName", account.FirstName);
                cmd.Parameters.AddWithValue("@Surname", account.Surname);
                cmd.Parameters.AddWithValue("@Email", account.Email);
                cmd.Parameters.AddWithValue("@Phone", account.Phone);
                cmd.Parameters.AddWithValue("@AddressLine1", account.AddressLine1);
                cmd.Parameters.AddWithValue("@AddressLine2", account.AddressLine2);
                cmd.Parameters.AddWithValue("@City", account.City);
                cmd.Parameters.AddWithValue("@County", account.County);
                cmd.Parameters.AddWithValue("@AccountType", account.AccountType);
                cmd.Parameters.AddWithValue("@SortCode", account.SortCode);
                cmd.Parameters.AddWithValue("@Balance", account.Balance);
                cmd.Parameters.AddWithValue("@OverdraftLimit", account.OverdraftLimit);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateAccount(Account account)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateAccount", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
                cmd.Parameters.AddWithValue("@Email", account.Email);
                cmd.Parameters.AddWithValue("@Phone", account.Phone);
                cmd.Parameters.AddWithValue("@AddressLine1", account.AddressLine1);
                cmd.Parameters.AddWithValue("@AddressLine2", account.AddressLine2);
                cmd.Parameters.AddWithValue("@City", account.City);
                cmd.Parameters.AddWithValue("@County", account.County);
                cmd.Parameters.AddWithValue("@OverdraftLimit", account.OverdraftLimit);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Deposit(int accountNumber, decimal amount)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_Deposit", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                cmd.Parameters.AddWithValue("@Amount", amount);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Withdraw(int accountNumber, decimal amount)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_Withdraw", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                cmd.Parameters.AddWithValue("@Amount", amount);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}