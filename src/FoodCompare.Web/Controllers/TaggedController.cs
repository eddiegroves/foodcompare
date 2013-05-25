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
            ViewBag.Tag = request;
            return View(foods);
        }
    }
}
