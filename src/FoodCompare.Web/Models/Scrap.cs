using ServiceStack.DataAnnotations;

namespace FoodCompare.Web.Models
{
    public class Scrap
    {
        [AutoIncrement]
        public int Id { get; set; }

        [References(typeof(Food))]
        public int? FoodId { get; set; }
        public string Url { get; set; }
        public string BrandXPath { get; set; }
        public string ProductXPath { get; set; }
        public string CaloriesXPath { get; set; }
        public string CarbohydrateXPath { get; set; }
        public string ProteinXPath { get; set; }
        public string FatXPath { get; set; }

        public Food Preview { get; set; }
    }
}