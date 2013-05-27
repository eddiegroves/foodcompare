using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodCompare.Web.Models
{
    public class Food
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Product { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}")]
        public decimal Calories { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}")]
        public decimal Carbohydrate { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:0.#}")]
        public decimal Protein { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}")]
        public decimal Fat { get; set; }
        public string Tags { get; set; }
        public decimal Price { get; set; }
    }
}