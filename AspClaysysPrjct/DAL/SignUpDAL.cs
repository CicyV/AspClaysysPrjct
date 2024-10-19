using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AspClaysysPrjct.Models;

namespace AspClaysysPrjct.DAL
{
    public class SignUpDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ConnectionString;

        public SignUpModel GetProfileById(int id) // Use SignUpModel
        {
            SignUpModel signUp1 = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SPS_signUpById1", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);

                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    signUp1 = new SignUpModel // Correct object instantiation
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        FullName = reader["fullName"].ToString(),
                        DateOfBirth = reader["dateOfBirth"].ToString(),
                        Gender = reader["gender"].ToString(),
                        Email = reader["email"].ToString(),
                        PhoneNumber = reader["phoneNumber"].ToString(),
                        Address = reader["address"].ToString(),
                        State = reader["state"].ToString(),
                        City = reader["city"].ToString(),
                        UserName = reader["userName"].ToString(),
                        DecryptedPassword = reader["DecryptedPassword"].ToString()
                    };
                }

                sqlConnection.Close();
            }

            return signUp1; // Return SignUpModel
        }
    }
}