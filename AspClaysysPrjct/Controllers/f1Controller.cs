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
    public class f1Controller : Controller
    {
        private f1_DAL userDAL = new f1_DAL();
        // GET: f1
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyProfile()
        {
            try
            {

                int id = (int)Session["UserId"];
                var signUp= userDAL.GetUserProfile(id);
                return View(signUp);
                //SignUp userProfile = userDAL.GetUserProfile(id);
                //if (userProfile == null)
                //{
                //    return HttpNotFound();
                //}

                //return View(userProfile);
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