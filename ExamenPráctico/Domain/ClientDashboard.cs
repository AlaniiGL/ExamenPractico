using ExamenPráctico.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Dapper;
using System.Transactions;

namespace ExamenPráctico.Domain
{
    public class ClientDashboard
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DBContext"].ToString();

        public List<ClientModel> GetUsers(ClientViewModel viewModel)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                string query = @" SELECT * FROM client ";

                if (!string.IsNullOrEmpty(viewModel.SearchQuery))
                {
                    viewModel.SearchQuery = $"%{viewModel.SearchQuery}%";
                    query += @"
                        WHERE (CONCAT(client.ClientName, ' ', client.ClientLastName1, ' ', client.ClientLastName2) LIKE @SearchQuery OR 
                            client.Id_Client LIKE @SearchQuery)
                    ";
                }

                var users = connection.Query<ClientModel>(query).ToList();

                connection.Close();
                return users;
            }
        }
        public ClientModel GetUser(ulong? id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                string query = @" SELECT * FROM client WHERE Id_Client = @id ";

                var client = connection.Query<ClientModel>(query, new { id }).SingleOrDefault();

                connection.Close();
                return client;
            }
        }

        public void AddClient(ClientModel viewModel)
        {
            using (var scope = new TransactionScope())
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    string query = @" INSERT INTO client (ClientName, ClientLastName1, ClientLastName2, ClientCreateDate, ClientUpdateDate) 
                        VALUES (@ClientName, @ClientLastName1, @ClientLastName2, NOW(), NOW()) ";

                    connection.Execute(query, viewModel);
                }
            }
        }
    }
}