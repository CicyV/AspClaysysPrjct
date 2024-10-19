using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AspClaysysPrjct.Models;
using System.Collections.Generic;
using System.Linq;

namespace AspClaysysPrjct.DAL
{
    public class Admin_DAL
    {

        private string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();

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
        // Get all users
        public List<SignUp> GetAllUser()
        {
            List<SignUp> signUpList = new List<SignUp>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                //using (SqlCommand sqlConnection = new SqlCommand("SPS_SignUp", connection))
                //{
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                // command.CommandText = "SPS_SignUp";//SPS_signUp1
                command.CommandText = "SPS_signUp1";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtSignUp = new DataTable();

                connection.Open();
                sqlDA.Fill(dtSignUp);
                connection.Close();

                foreach (DataRow dr in dtSignUp.Rows)
                {
                    signUpList.Add(new SignUp
                    {
                        id = Convert.ToInt32(dr["id"]),
                        fullName = dr["fullName"].ToString(),
                        dateOfBirth = dr["dateOfBirth"].ToString(),
                       // dateOfBirth= Convert.ToDateTime(dr["dateOfBirth"]),
                        gender = dr["gender"].ToString(),
                        email = dr["email"].ToString(),
                        phoneNumber = dr["phoneNumber"].ToString(),
                        address = dr["address"].ToString(),
                        state = dr["state"].ToString(),
                        city = dr["city"].ToString(),
                        userName = dr["userName"].ToString(),
                        //password = dr["password"].ToString()//DecryptedPassword
                        password = dr["DecryptedPassword"].ToString()
                    });
                }

            }
            return signUpList;
        }
        public void DeleteSignUp(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SPD_DeleteSignUp", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void InsertFoodItem(FoodItem foodItem)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SPI_FoodItem", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@imagePath", foodItem.imagePath);
                    cmd.Parameters.AddWithValue("@foodName", foodItem.foodName);
                    cmd.Parameters.AddWithValue("@description", foodItem.description);
                    cmd.Parameters.AddWithValue("@price", foodItem.price);
                    cmd.Parameters.AddWithValue("@quantity", foodItem.quantity);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<FoodItem> GetAllFoodItems()
        {
            List<FoodItem> foodItems = new List<FoodItem>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SPS_FoodItems", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        foodItems.Add(new FoodItem
                        {
                            foodId = Convert.ToInt32(reader["foodId"]),
                            imagePath = reader["imagePath"].ToString(),
                            foodName = reader["foodName"].ToString(),
                            description = reader["description"].ToString(),
                            price = Convert.ToDecimal(reader["price"]),
                            quantity = Convert.ToInt32(reader["quantity"])
                        });
                    }
                }
            }
            return foodItems;
        }
        // Get FoodItem by FoodId
        public FoodItem GetFoodItemById(int id)
        {
            FoodItem foodItem = null;

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SPS_GetFoodItemById1", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@foodId", id);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        foodItem = new FoodItem
                        {
                            foodId = Convert.ToInt32(reader["foodId"]),
                            imagePath = reader["imagePath"].ToString(),
                            foodName = reader["foodName"].ToString(),
                            description = reader["description"].ToString(),
                            price = Convert.ToDecimal(reader["price"]),
                            quantity = Convert.ToInt32(reader["quantity"])
                        };
                    }
                }
            }

            return foodItem;
        }
        // display order details
        public List<OrderModel> GetAllOrder()
        {
            List<OrderModel> orderList = new List<OrderModel>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand sqlConnection = new SqlCommand("SPS_GetOrder", connection))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SPS_GetOrder";
                    SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                    DataTable dtOrder = new DataTable();

                    connection.Open();
                    sqlDA.Fill(dtOrder);
                    connection.Close();
                    foreach (DataRow dr in dtOrder.Rows)
                    {
                        orderList.Add(new OrderModel
                        {


                            OrderId = Convert.ToInt32(dr["orderId"]),
                            FoodId = Convert.ToInt32(dr["foodId"]),
                            Qty = Convert.ToInt32(dr["qty"]),
                            UserAddress = dr["userAddress"].ToString(),
                            PaymentMode = dr["paymentMode"].ToString()
                        });
                    }
                }
            }
            return orderList;
        }

        // Method to delete an order by OrderId
        public void DeleteOrder(int orderId)
        {
            using (var connection = new SqlConnection(conString))
            {
                using (var command = new SqlCommand("SPD_DeleteOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@orderId", orderId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteFoodItem(int foodId)
        {
            using (var connection = new SqlConnection(conString))
            {
                using (var command = new SqlCommand("SPD_DeleteFoodItems", connection))
               
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@foodId", foodId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }











    }
}