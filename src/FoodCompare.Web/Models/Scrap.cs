using ServiceStack.DataAnnotations;

namespace FoodCompare.Web.Models
{
    public class Scrap
    {
        [AutoIncrement]
        public int Id { get; set; }

        [References(typeof(Food))]
        public int FoodId { get; set; }
        public string Url { get; set; }
    }
}