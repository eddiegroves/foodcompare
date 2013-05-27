using System.Collections.Generic;

namespace FoodCompare.Web.Models
{
    public class TaggedFoodViewModel
    {
        public string TagName { get; set; }
        public List<Food> Foods { get; set; }
        public Food Totals { get; set; }
    }
}