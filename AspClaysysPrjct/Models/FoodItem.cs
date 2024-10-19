using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspClaysysPrjct.Models
{
    public class FoodItem
    {
        public int foodId { get; set; }
        public string imagePath { get; set; }
        public string foodName { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public HttpPostedFileBase UploadFile { get; set; }  // For image upload

        internal object FirstOrDefault()
        {
            throw new NotImplementedException();
        }
    }
}