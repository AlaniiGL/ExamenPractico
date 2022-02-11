using ExamenPráctico.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ExamenPráctico.Domain
{
    public class TransactionsDashboard
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DBContext"].ToString();

        public TransactionTypeModel GetTransactionType(ulong id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                string query = @" SELECT * FROM transactiontype WHERE Id_TransactionType = @id ";

                var type = connection.Query<TransactionTypeModel>(query, new { id }).SingleOrDefault();

                return type;
            }
        }

        public List<TransactionModel> GetTransactions (ulong? id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                string query = @" SELECT transactions.Id_Transaction, transactions.Id_Account, CONCAT(client.ClientName, ' ', client.ClientLastName2, ' ', ClientLastName2) AS FullName, 
                    transactiontype.TransactionName, transactions.Transaction_Quantity, transactions.Previous_Balance, transactions.Subsecuent_Balance, transactions.Transaction_Date
                    FROM transactions 
                    INNER JOIN account ON account.Id_Account = transactions.Id_Account
                    INNER JOIN client ON client.Id_Client = account.Id_Client
                    INNER JOIN transactiontype ON transactiontype.Id_TransactionType = transactions.Id_TransactionType
                    ";

                if (id.HasValue)
                {
                    query += " WHERE transactions.Id_Account LIKE @id ";
                }

                query += " ORDER BY transactions.Id_Transaction ";

                var record = connection.Query<TransactionModel>(query, new { id }).ToList();

                return record;
            }
        }
    }
}