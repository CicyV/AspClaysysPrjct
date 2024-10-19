using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspClaysysPrjct.DAL;
using AspClaysysPrjct.Models;

namespace AspClaysysPrjct.Controllers
{
    public class HomeController : Controller
    {
        private readonly Home_DAL _signUpDAL = new Home_DAL();
        Home_DAL _contactDAL = new Home_DAL();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// for user signUp
        /// </summary>
        /// <returns></returns>
        public ActionResult Registeration()
        {
            ViewBag.StateList = new SelectList(GetStateList(), "Value", "Text");  // Convert to SelectList
            ViewBag.CityList = new SelectList(GetCityList(), "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registeration(SignUp signUp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = _signUpDAL.InsertAccount(signUp);
                    Session["UserId"] = id;
                    TempData["SuccessMessage"] = "Registration Successful!";
                    //  return RedirectToAction("Login", "Home");
                    return RedirectToAction("Index", "Login");
                }

                ViewBag.StateList = new SelectList(GetStateList(), "Value", "Text");
                ViewBag.CityList = new SelectList(GetCityList(), "Value", "Text");
                return View(signUp);
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Error during user registration", ex);
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;

                ViewBag.StateList = new SelectList(GetStateList(), "Value", "Text");
                ViewBag.CityList = new SelectList(GetCityList(), "Value", "Text");
                return View(signUp);
            }
        }
        public List<SelectListItem> GetStateList()
        {
            return new List<SelectListItem>
             {
                new SelectListItem { Text = "New York", Value = "NY" },
                new SelectListItem { Text = "California", Value = "CA" },
                new SelectListItem { Text = "Texas", Value = "TX" },
                new SelectListItem { Text = "Florida", Value = "FL" },
                new SelectListItem { Text = "Illinois", Value = "IL" }
             };
        }
        public List<SelectListItem> GetCityList()
        {
            return new List<SelectListItem>
             {
                new SelectListItem { Text = "City1", Value = "C1" },
                new SelectListItem { Text = "City2", Value = "C2" },
                new SelectListItem { Text = "City3", Value = "C3" },
                new SelectListItem { Text = "City4", Value = "C4" },
                new SelectListItem { Text = "City5", Value = "C5" }
             };
        }

        /// <summary>
        /// login to hompage
        /// </summary>
        /// <returns></returns>
        // GET: Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginView model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Username == "admin" && model.Password == "admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "User");
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Error during login", ex);
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
                return View(model);
            }
        }

        /// <summary>
        /// logut registered users from application 
        /// </summary>
        /// <returns></returns>
        // GET: Account/Logout
        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                return RedirectToAction("Index","Login");
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Error during logout", ex);
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: ContactUs/Create
        public ActionResult Contactcreate()
        {
            return View();
        }



        /// <summary>
        /// user can view contact information
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>View(contact)</returns>
        // POST: ContactUs/Create
        [HttpPost]
        public ActionResult Contactcreate(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isInserted = _contactDAL.InsertContact(contact);
                    if (isInserted)
                    {
                        TempData["SuccessMessage"] = "Successful!";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unsuccessful!";
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(contact);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Error during contact creation", ex);
                TempData["ErrorMessage"] = ex.Message;
                return View(contact);
            }
        }









    }
}