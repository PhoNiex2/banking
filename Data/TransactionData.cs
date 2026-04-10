using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using banking.Models;

namespace banking.Data
{
    public class TransactionData
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        public void AddTransaction(TransactionRecord transaction)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_AddTransaction", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SourceAccountNumber", transaction.SourceAccountNumber);
                cmd.Parameters.AddWithValue("@DestinationAccountNumber",
                    (object)transaction.DestinationAccountNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TransactionType", transaction.TransactionType);
                cmd.Parameters.AddWithValue("@Amount", transaction.Amount);
                cmd.Parameters.AddWithValue("@ReferenceNumber", transaction.ReferenceNumber);
                cmd.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<TransactionRecord> GetTransactionsByAccount(int accountNumber)
        {
            List<TransactionRecord> list = new List<TransactionRecord>();

            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_GetTransactionsByAccount", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new TransactionRecord
                    {
                        TransactionID = (int)reader["TransactionID"],
                        SourceAccountNumber = (int)reader["SourceAccountNumber"],
                        DestinationAccountNumber = reader["DestinationAccountNumber"] == DBNull.Value
                            ? (int?)null
                            : (int)reader["DestinationAccountNumber"],
                        TransactionType = reader["TransactionType"].ToString(),
                        Amount = (decimal)reader["Amount"],
                        ReferenceNumber = reader["ReferenceNumber"].ToString(),
                        TransactionDate = (DateTime)reader["TransactionDate"]
                    });
                }
            }

            return list;
        }
    }
}