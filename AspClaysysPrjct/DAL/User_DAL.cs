using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AspClaysysPrjct.Models;
using System.Collections.Generic;
using System.Linq;

namespace AspClaysysPrjct.DAL
{
    public class User_DAL
    {


        private string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();
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
        //public int PlaceOrder(OrderModel order)
        //{
        //    using (SqlConnection con = new SqlConnection(conString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SPI_Order", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@id", order.UserId);
        //        cmd.Parameters.AddWithValue("@foodId", order.FoodId);
        //        cmd.Parameters.AddWithValue("@qty", order.Quantity);
        //        cmd.Parameters.AddWithValue("@userAddress", order.UserAddress);
        //        cmd.Parameters.AddWithValue("@paymentMode", order.PaymentMode);

        //        con.Open();
        //        int totalPrice = (int)cmd.ExecuteScalar();  // Retrieve the total price from the stored procedure result
        //        con.Close();

        //        return totalPrice;
        //    }
        //}
        public int PlaceOrder(OrderModel order, out int totalPrice)
        {
            totalPrice = 0;

            using (SqlConnection conn = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SPI_InsertOrder1", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@foodId", order.FoodId);
                    cmd.Parameters.AddWithValue("@qty", order.Qty);
                    cmd.Parameters.AddWithValue("@userAddress", order.UserAddress);
                    cmd.Parameters.AddWithValue("@paymentMode", order.PaymentMode);

                    SqlParameter totalPriceParam = new SqlParameter("@totalPrice", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(totalPriceParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    totalPrice = (int)totalPriceParam.Value;
                }
            }

            return totalPrice;
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
        public SignUp GetUserProfile(int id)
        {
            SignUp userProfile = null;

            using (var connection = new SqlConnection(conString))
            {
                using (var command = new SqlCommand("GetUserProfile", connection))
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
