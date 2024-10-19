using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspClaysysPrjct.Models
{
    public class OrderModel
    {


        //public int OrderId { get; set; }
        //public int UserId { get; set; }  // Foreign key to tbl_SignUp
        //public int FoodId { get; set; }  // Foreign key to tbl_FoodItem
        //public int Quantity { get; set; }
        //public int TotalPrice { get; set; }
        //public string UserAddress { get; set; }
        //public string PaymentMode { get; set; }

        public int OrderId { get; set; }
        public int FoodId { get; set; }
            public int Qty { get; set; }
            public string UserAddress { get; set; }
            public string PaymentMode { get; set; }
        

    }
}