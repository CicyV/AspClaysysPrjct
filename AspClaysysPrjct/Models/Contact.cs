using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspClaysysPrjct.Models
{
    public class Contact
    {
        [Key]
        public int contactId { get; set; }
        [Required]
        public string fullName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string message { get; set; }
    }
}



