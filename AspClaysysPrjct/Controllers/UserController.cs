using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspClaysysPrjct.DAL;
using AspClaysysPrjct.Models;

namespace AspClaysysPrjct.Controllers
{
    public class UserController : Controller
    {
        private User_DAL foodItemDAL = new User_DAL();
        private User_DAL orderDal = new User_DAL();
        private User_DAL userDAL = new User_DAL();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// User can view fooditem list
        /// </summary>
        /// <returns>View(foodItems)</returns>
        public ActionResult ViewFoodItem()
        {
            try
            {
                var foodItems = foodItemDAL.GetAllFoodItems();
                return View(foodItems);
            }
            catch (Exception ex)
            {
                // Log the error
                LogHelper.LogError("Error while retrieving food items.", ex);
                ViewBag.Message = "An error occurred while retrieving food items.";
                return View(new List<FoodItem>()); // Return empty list if error occurs
            }
        }

        /// <summary>
        /// after selecting particular food for order.display order for for particular foo item
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns> View(model)</returns>
        // GET: Displays the order form
        public ActionResult OrderCreate(int foodId)
        {
            try
            {
                var model = new OrderModel { FoodId = foodId };
                return View(model);
            }
            catch (Exception ex)
            {
                // Log the error
                LogHelper.LogError($"Error while preparing order form for food ID: {foodId}.", ex);
                ViewBag.Message = "An error occurred while preparing the order form.";
                return View(new OrderModel());
            }
        }

        //// POST: Handles order placement
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult OrderCreate(OrderModel order)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            int totalPrice;
        //            orderDal.PlaceOrder(order, out totalPrice);

        //            ViewBag.Message = "Order placed successfully!";
        //            ViewBag.TotalPrice = totalPrice;

        //            return View("OrderConfirmation");
        //        }

        //        return View(order);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Message = "An error occurred: " + ex.Message;
        //        return View(order);
        //    }
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderCreate(OrderModel order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int totalPrice;
                    orderDal.PlaceOrder(order, out totalPrice);

                    ViewBag.Message = "Order placed successfully!";
                    ViewBag.TotalPrice = totalPrice;

                    return View("OrderConfirmation");
                }

                return View(order);
            }
            catch (SqlException ex) // Catch specific SQL exceptions
            {
                LogHelper.LogError("SQL error while placing order.", ex); // Log SQL exception
                if (ex.Message.Contains("Not enough stock"))
                {
                    ViewBag.Message = "Sorry, not enough stock available for this item.";
                }
                else
                {
                    ViewBag.Message = "An error occurred while placing the order: " + ex.Message;
                }
                return View(order);
            }
            catch (Exception ex)
            {
                LogHelper.LogError("General error while placing order.", ex); // Log general exceptions
                ViewBag.Message = "An error occurred while placing the order: " + ex.Message;
                return View(order);
            }
        }

        /// <summary>
        /// after place order confirmation messsage will display
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderConfirmation()
        {
            return View();
        }

        /// <summary>
        /// ordered list can view
        /// </summary>
        /// <returns>View(OrderList)</returns>
        public ActionResult ViewOrder()
        {
            try
            {
                var orderList = orderDal.GetAllOrder();
                return View(orderList);
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Error while retrieving orders.", ex); // Log error
                ViewBag.Message = "An error occurred while retrieving orders.";
                return View(new List<OrderModel>()); // Return empty list if error occurs
            }
        }




        /// <summary>
        /// delete particular order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // Action to delete an order
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                orderDal.DeleteOrder(id);
                TempData["SuccessMessage"] = "Order deleted successfully!";
                return RedirectToAction("ViewOrder");
            }
            catch (Exception ex)
            {
                LogHelper.LogError($"Error while deleting order with ID: {id}.", ex); // Log error
                TempData["ErrorMessage"] = "An error occurred while deleting the order.";
                return RedirectToAction("ViewOrder");
            }
        }
        // Action to view user profile
        public ActionResult MyProfile()
        {
            try
            {
               
                int id = (int)Session["UserId"];
                SignUp userProfile = userDAL.GetUserProfile(id);
                if (userProfile == null)
                {
                    return HttpNotFound();
                }

                return View(userProfile);
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Error while retrieving user profile.", ex); // Log error
                ViewBag.Message = "An error occurred while retrieving your profile.";
                return View();
            }
        }













    }
}




