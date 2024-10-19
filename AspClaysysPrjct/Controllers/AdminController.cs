using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspClaysysPrjct.DAL;
using AspClaysysPrjct.Models;

namespace AspClaysysPrjct.Controllers
{
    public class AdminController : Controller
    {
        Admin_DAL _contactDAL = new Admin_DAL();
        Admin_DAL _adminDAL = new Admin_DAL();
        Admin_DAL foodItemDAL = new Admin_DAL();
        Admin_DAL orderDal = new Admin_DAL();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// admin can able to view  feedback list from user
        /// </summary>
        /// <returns>View(contactList)</returns>
        public ActionResult Contact()
        {
            var contactList = _contactDAL.GetAllContact();
            return View(contactList);
        }
        /// <summary>
        /// Admin can view registered user list
        /// </summary>
        /// <returns>View(signUpList)</returns>
        public ActionResult ViewUser()
        {
            //var signUpList = _adminDAL.GetAllUser();
            //return View(signUpList);

            try
            {
                var signUpList = _adminDAL.GetAllUser();
                return View(signUpList);
            }
            catch (Exception ex)
            {
                // Log the error
                LogHelper.LogError("Failed to retrieve user list", ex);

                // Optionally, show a friendly message to the user
                TempData["ErrorMessage"] = "An error occurred while loading users.";
                return RedirectToAction("Index");
            }
        }



        /// <summary>
        /// Admin can delete particular user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ViewUser</returns>
        public ActionResult DeleteUser(int id)
        {

            return View(id);
        }

        // POST: SignUp/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int id, FormCollection collection)
        {
            
            try
            {
                _adminDAL.DeleteSignUp(id);
                return RedirectToAction("ViewUser");
            }
            catch (Exception ex)
            {
                // Log the error
                LogHelper.LogError($"Failed to delete user with ID: {id}", ex);

                TempData["ErrorMessage"] = "An error occurred while deleting the user.";
                return View(id);
            }
        }

        /// <summary>
        /// Admin can able toinsert new food items
        /// </summary>
        /// <returns> displayFood</returns>
       
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodItem model)
       
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.UploadFile != null)
                    {
                        using (var binaryReader = new BinaryReader(model.UploadFile.InputStream))
                        {
                            byte[] imageBytes = binaryReader.ReadBytes(model.UploadFile.ContentLength);
                            model.imagePath = Convert.ToBase64String(imageBytes);
                        }
                    }

                    _adminDAL.InsertFoodItem(model);
                    return RedirectToAction("displayFood");
                }
            }
            catch (Exception ex)
            {
                // Log the error
                LogHelper.LogError("Failed to create food item", ex);

                TempData["ErrorMessage"] = "An error occurred while creating the food item.";
            }
            return View(model);
        }

        /// <summary>
        /// Admin can view Food details
        /// </summary>
        /// <returns></returns>
        public ActionResult displayFood()
        {
            var foodItems = foodItemDAL.GetAllFoodItems();
            return View(foodItems);
        }
        //[HttpPost]
        //public ActionResult PlaceOrder(int foodId, int qty, string userAddress, string paymentMode)
        //{

        //    int userId = Convert.ToInt32(Session["id"]);

        //    Order order = new Order
        //    {
        //        id = userId,
        //        foodId = foodId,
        //        qty = qty,
        //        userAddress = userAddress,
        //        paymentMode = paymentMode
        //    };

        //    foodItemDAL.InsertOrder(order);

        //    return RedirectToAction("displayFood");
        //}


        /// <summary>
        /// Admin can able to delete particular food item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>displayFood</returns>

        // GET: FoodItem/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                foodItemDAL.DeleteFoodItem(id);

                return RedirectToAction("displayFood");
            }
            catch
            {
                return View();
            }
        }


        //// GET: FoodItem/Delete/5
        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    id = 1;
        //    return View(id);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        id = 1;
        //        foodItemDAL.DeleteFoodItem(id);

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        /// <summary>
        /// Admin can view Order Details
        /// </summary>
        /// <returns>View(OrderList)</returns>

        public ActionResult ViewOrder()
        {
            var OrderList = orderDal.GetAllOrder();
            return View(OrderList);
        }




        /// <summary>
        /// Admin can delete order
        /// </summary>
        /// <param name="id"></param>
        /// <returns> RedirectToAction("ViewOrder")</returns>
        // Action to delete an order
        public ActionResult DeleteOrder(int id)
        {
            orderDal.DeleteOrder(id);
            TempData["SuccessMessage"] = "Order deleted successfully!";
            return RedirectToAction("ViewOrder");
        }

        /// <summary>
        /// to select particular food item 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult FoodDetails(int id)
        {
            try
            {
                var foodItem = foodItemDAL.GetFoodItemById(id).FirstOrDefault();
                if (foodItem == null)
                {
                    TempData["InfoMessage"] = "food not available";
                    return RedirectToAction("Index");

                }
                return View(foodItem);
            }
            catch (Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }


    }
}