using ExamenPráctico.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using System.Web;

namespace ExamenPráctico.Domain
{
    public class AccountDashboard
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DBContext"].ToString();

        public ClientModel GetUserData(ulong? id, string searchQuery)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                string query = @" SELECT Id_Client, ClientName, ClientLastName1, ClientLastName2 FROM client ";

                if (id.HasValue)
                {
                    query += " WHERE Id_Client = @id ";
                }

                else if (!string.IsNullOrEmpty(searchQuery))
                {
                    searchQuery = $"%{searchQuery}%";
                    query += @"
                        WHERE (CONCAT(client.ClientName, ' ', client.ClientLastName1, ' ', client.ClientLastName2) LIKE @searchQuery OR 
                            client.Id_Client LIKE @searchQuery)
                    ";
                }

                var user = connection.Query<ClientModel>(query, new { id, searchQuery }).SingleOrDefault();

                return user;
            }
        }

        public void AddAccount(CreateSavingAccountModel viewModel)
        {
            using (var scope = new TransactionScope())
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    string query = @" INSERT INTO account (Id_Client, Account_Balance, AccountCreateDate, AccountUpdateDate) VALUES (@Id_Client, @Account_Balance, NOW(), NOW()) ";

                    connection.Execute(query, viewModel);
                }

                scope.Complete();
            }
        }

        public List<ShowAccountDetailsModel> ViewUserAccounts(ulong id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                string query = @" SELECT client.Id_Client, account.Id_Account, account.AccountCreateDate, client.ClientName, client.ClientLastName1, client.ClientLastName2
                    FROM client
                    INNER JOIN account ON account.Id_Client = client.Id_Client
                    WHERE client.Id_Client = @id                    
                    ";

                var accounts = connection.Query<ShowAccountDetailsModel>(query, new { id }).ToList();
                return accounts;
            }
        }

        public ShowAccountDetailsModel GetAccount(ulong account)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                string query = @" SELECT Id_Account, Account_Balance, AccountCreateDate FROM account WHERE Id_Account = @account ";

                var userAccount = connection.Query<ShowAccountDetailsModel>(query, new { account }).SingleOrDefault();
                return userAccount;
            }
        }

        public CreateSavingAccountModel GetDataAccount(ulong id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                string query = @" SELECT * FROM account WHERE Id_Account = @id ";

                var account = connection.Query<CreateSavingAccountModel>(query, new { id }).SingleOrDefault();

                return account;
            }

        }

        public void MoneyInOut(EditAccountModel data)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                string account = @" UPDATE account SET 
                    Account_Balance = @Amount,
                    AccountUpdateDate = NOW()
                    ";

                connection.Execute(account, data);

                string transaction = @" INSERT INTO transactions (
                    Id_Account,
                    Id_TransactionType,
                    Transaction_Quantity,
                    Previous_Balance,
                    Subsecuent_Balance,
                    Transaction_Date
                    ) VALUES (
                    @Id_Account,
                    @Id_TransactionType,
                    @TransactionAmount,
                    @Previous_Balance,
                    @Amount,
                    NOW()                    
                    )
                    ";

                connection.Execute(transaction, data);
            }
        }
    }
}