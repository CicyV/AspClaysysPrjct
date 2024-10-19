using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AspClaysysPrjct.Models;
using System.Collections.Generic;
using System.Linq;

namespace AspClaysysPrjct.DAL
{
    public class f1_DAL
    {
        private string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();

        public SignUp GetUserProfile(int id)
        {
            SignUp userProfile = null;

            using (var connection = new SqlConnection(conString))
            {
                using (var command = new SqlCommand("SPS_signUpById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userProfile = new SignUp
                            {
                                id = reader.GetInt32(0),
                                fullName = reader.GetString(1),
                                dateOfBirth = reader.GetString(2),
                                gender = reader.GetString(3),
                                email = reader.GetString(4),
                                phoneNumber = reader.GetString(5),
                                address = reader.GetString(6),
                                state = reader.GetString(7),
                                city = reader.GetString(8),
                                userName = reader.GetString(9)
                            };
                        }
                    }
                }
            }

            return userProfile;
        }
    }
}