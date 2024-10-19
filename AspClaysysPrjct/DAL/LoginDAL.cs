using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AspClaysysPrjct.Models;
using System.Linq;
using System.Web;

namespace AspClaysysPrjct.DAL
{
    public class LoginDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ConnectionString;

        public int VerifyLogin(string userName, string password, out string fullName)
        {
            int userId = -1;
            fullName = string.Empty;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SPS_VerifyLogin", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@userName", userName);
                command.Parameters.AddWithValue("@password", password);

                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    userId = Convert.ToInt32(reader["id"]);
                    fullName = reader["fullName"].ToString();
                }

                sqlConnection.Close();
            }

            return userId;
        }
    }

}