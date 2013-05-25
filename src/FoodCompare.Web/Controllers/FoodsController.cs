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
    public class FoodsController : BaseController
    {
        private readonly IDbConnection db;

        public FoodsController(IDbConnection db) : base(db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {
            return View(db.Select<Food>());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Food request)
        {
            db.Insert(request);
            return View(request);
        }

        public ActionResult Edit(int id)
        {
            return View(db.GetById<Food>(id));
        }

        [HttpPost]
        public ActionResult Edit(Food request)
        {
            db.Update(request);
            return View(request);
        }

        public ActionResult Delete(int id)
        {
            return View(db.GetById<Food>(id));
        }

        [HttpPost]
        public ActionResult Delete(Food request)
        {
            db.Delete<Food>(f => f.Id == request.Id);
            return RedirectToAction("Index");
        }
    }
}
