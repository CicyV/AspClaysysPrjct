using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspClaysysPrjct.Models
{
    public class SignUp
    {
        [Key]
        public int id { get; set; }
       
        public string fullName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public string dateOfBirth { get; set; }

        public string gender { get; set; }

        public string email { get; set; }
        //[Required]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "The phone number must be numeric.")]

        public string phoneNumber { get; set; }

        public string address { get; set; }

        public string state { get; set; }


        public string city { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        public string CPassword { get; set; }
    }
}