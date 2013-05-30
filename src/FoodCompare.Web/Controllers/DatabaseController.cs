using FoodCompare.Web.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodCompare.Web.Controllers
{
    public class DatabaseController : BaseController
    {
        public DatabaseController()
        { 
        }

        public DatabaseController(IDbConnection db) : base(db) { }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Clear()
        {
            Database.Create();
            return RedirectToAction("Index");
        }
    }
}
