using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AspClaysysPrjct.Models;
using System.Collections.Generic;
using System.Linq;

namespace AspClaysysPrjct.DAL
{
    public class Home_DAL
    {
        private string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();
        
        public int InsertAccount(SignUp signUp)
        {
            int userId = 0;

            using (SqlConnection con = new SqlConnection(conString))
            {
               // using (SqlCommand cmd = new SqlCommand("SPI_InsertAccount", con))  //SPI_SignUpTable
                using (SqlCommand cmd = new SqlCommand("SPI_SignUpTable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", signUp.fullName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", signUp.dateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", signUp.gender);
                    cmd.Parameters.AddWithValue("@Email", signUp.email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", signUp.phoneNumber);
                    cmd.Parameters.AddWithValue("@Address", signUp.address);
                    cmd.Parameters.AddWithValue("@State", signUp.state);
                    cmd.Parameters.AddWithValue("@City", signUp.city);
                    cmd.Parameters.AddWithValue("@UserName", signUp.userName);
                    cmd.Parameters.AddWithValue("@Password", signUp.password);

                    //SqlParameter outputId = new SqlParameter("@UserId", SqlDbType.Int)
                    //{
                    //    Direction = ParameterDirection.Output
                    //};
                    //cmd.Parameters.Add(outputId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                  //  userId = (int)outputId.Value;
                }
            }

            return userId;
        }
        // Get all contacts
        public List<Contact> GetAllContact()
        {
            List<Contact> contactList = new List<Contact>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand sqlConnection = new SqlCommand("SPS_AllContactUs", connection))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SPS_AllContactUs";
                    SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                    DataTable dtContact = new DataTable();

                    connection.Open();
                    sqlDA.Fill(dtContact);
                    connection.Close();

                    foreach (DataRow dr in dtContact.Rows)
                    {
                        contactList.Add(new Contact
                        {
                            //contactId = Convert.ToInt32(dr["contactId"]),
                            fullName = dr["fullName"].ToString(),
                            email = dr["email"].ToString(),
                            phoneNumber = dr["phoneNumber"].ToString(),
                            message = dr["message"].ToString()
                        });
                    }
                }
            }
            return contactList;
        }



        public bool InsertContact(Contact contact)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(conString))
                {
                    SqlCommand command = new SqlCommand("SPI_ContactUS", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@fullName", contact.fullName);
                    command.Parameters.AddWithValue("@email", contact.email);
                    command.Parameters.AddWithValue("@phoneNumber", contact.phoneNumber);
                    command.Parameters.AddWithValue("@message", contact.message);

                    sqlConnection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    sqlConnection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                // Log exception (not shown here for simplicity)
                return false;
            }

        }











    }
}