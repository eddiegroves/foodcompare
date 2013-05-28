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
    public class ScrapsController : BaseController
    {
        private readonly IDbConnection _db;

        public ScrapsController(IDbConnection db) : base(db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Select<Scrap>());
        }

        public ActionResult Add()
        {
            ViewBag.Foods = _db.Select<Food>();
            return View(new Scrap());
        }

        [HttpPost]
        public ActionResult Add(Scrap scrap)
        {
            _db.Insert(scrap);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Foods = _db.Select<Food>();
            return View(_db.GetById<Scrap>(id));
        }

        [HttpPost]
        public ActionResult Edit(Scrap scrap)
        {
            _db.Update(scrap);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(_db.GetById<Scrap>(id));
        }

        [HttpPost]
        public ActionResult Delete(Scrap scrap)
        {
            _db.DeleteById<Scrap>(scrap.Id);
            return RedirectToAction("Index");
        }

        public ActionResult Run(int id)
        {
            var scrap = _db.GetById<Scrap>(id);
            ViewBag.Food = _db.GetById<Food>(scrap.FoodId);
            return View(scrap);
        }
    }
}
