//using Claysys.Models;
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Web.Mvc;
//using Claysys.DAL;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//public class LoginController : Controller
//{
//    private LoginDAL loginDAL = new LoginDAL();

//    // GET: Login
//    public ActionResult Index()
//    {
//        return View();
//    }

//    // POST: Login
//    [HttpPost]
//    public ActionResult Index(LoginModel model)
//    {
//        if (ModelState.IsValid)
//        {
//            string fullName;
//            int userId = loginDAL.VerifyLogin(model.UserName, model.Password, out fullName);

//            if (userId > 0)
//            {
//                // Store UserId and FullName in Session
//                Session["UserId"] = userId;
//                Session["FullName"] = fullName;

//                // Redirect to a secured page after login
//                return RedirectToAction("Dashboard", "Home");
//            }
//            else
//            {
//                ViewBag.ErrorMessage = "Invalid username or password.";
//            }
//        }

//        return View(model);
//    }
//}
using AspClaysysPrjct.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using AspClaysysPrjct.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class LoginController : Controller
{
    private LoginDAL loginDAL = new LoginDAL();

    // GET: Login
    public ActionResult Index()
    {
        return View();
    }

    // POST: Login
    [HttpPost]
    public ActionResult Index(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            string fullName;
            int userId = loginDAL.VerifyLogin(model.UserName, model.Password, out fullName);

            if (userId > 0)
            {
                if (model.UserName == "admin" && model.Password == "admin123")
                {

                    // Store UserId and FullName in Session
                    Session["UserId"] = userId;
                    Session["FullName"] = fullName;
                    return RedirectToAction("Index", "Admin");

                }
                else
                {
                    Session["UserId"] = userId;
                    Session["FullName"] = fullName;
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
            }
        }
         return View(model);
    }
}
