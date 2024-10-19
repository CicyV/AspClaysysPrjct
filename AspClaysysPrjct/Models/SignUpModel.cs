using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspClaysysPrjct.Models
{
    public class SignUpModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string UserName { get; set; }
        public string DecryptedPassword { get; set; }
    }
}