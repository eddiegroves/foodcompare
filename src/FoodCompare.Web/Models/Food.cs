using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodCompare.Web.Models
{
    public class Food
    {
        public string Brand { get; set; }
        public string Product { get; set; }
        public string Calories { get; set; }
        public string Carbohydrate { get; set; }
        public string Protein { get; set; }
        public string Fat { get; set; }
        public string Tags { get; set; }
    }
}