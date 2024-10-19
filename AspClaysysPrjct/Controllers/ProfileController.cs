using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspClaysysPrjct.DAL;
using AspClaysysPrjct.Models;

namespace AspClaysysPrjct.Controllers
{
    public class ProfileController : Controller
    {
        private SignUpDAL signUpDAL = new SignUpDAL();

        // GET: Profile/ViewProfile
        public ActionResult ViewProfile()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            SignUpModel signUp = signUpDAL.GetProfileById(userId); // Correct return type

            if (signUp != null)
            {
                return View(signUp); // Pass SignUpModel to the view
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}