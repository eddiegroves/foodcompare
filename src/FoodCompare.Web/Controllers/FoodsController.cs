using FoodCompare.Web.Models;
using FoodCompare.Web.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace FoodCompare.Web.Controllers
{
    public class FoodsController : Controller
    {
        public ActionResult Index()
        {
            using (IDbConnection db = Database.Factory.OpenDbConnection())
            {
                var foods = db.Select<Food>();
                return View(foods);
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Food request)
        {
            using (IDbConnection db = Database.Factory.OpenDbConnection())
            {
                db.Insert(request);
            }

            return View(request);
        }

        public ActionResult Edit(int id)
        {
            using (IDbConnection db = Database.Factory.OpenDbConnection())
            {
                return View(db.GetById<Food>(id));
            }
        }

        [HttpPost]
        public ActionResult Edit(Food request)
        {
            using (IDbConnection db = Database.Factory.OpenDbConnection())
            {
                db.Update(request);
            }

            return View(request);
        }

        public ActionResult Delete(int id)
        {
            using (IDbConnection db = Database.Factory.OpenDbConnection())
            {
                return View(db.GetById<Food>(id));
            }
        }

        [HttpPost]
        public ActionResult Delete(Food request)
        {
            using (IDbConnection db = Database.Factory.OpenDbConnection())
            {
                db.Delete<Food>(f => f.Id == request.Id);
            }

            return RedirectToAction("Index");
        }
    }
}
