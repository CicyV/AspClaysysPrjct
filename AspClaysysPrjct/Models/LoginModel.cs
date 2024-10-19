using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspClaysysPrjct.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}