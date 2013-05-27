using FoodCompare.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;

namespace FoodCompare.Web.Controllers
{
    public class TaggedController : BaseController
    {
        private readonly IDbConnection _db;

        public TaggedController(IDbConnection db) : base(db)
        {
            _db = db;
        }

        public ActionResult Index(Tag request)
        {
            var foods = _db.Select<Food>(p => p.Tags.Contains(request.Name));
            return View(new TaggedFoodViewModel
            {
                TagName =  request.Name,
                Foods = foods,
                Totals = new Food
                {
                    Product = "Total",
                    Calories = foods.Sum(f => f.Calories),
                    Carbohydrate = foods.Sum(f => f.Carbohydrate),
                    Protein = foods.Sum(f => f.Protein),
                    Fat = foods.Sum(f => f.Fat),
                    Price = foods.Sum(f => f.Price)
                }
            });
        }
    }
}
