using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using FoodCompare.Web.Models;

namespace FoodCompare.Web.Controllers
{
    public class TagsController : BaseController
    {
        private readonly IDbConnection _db;

        public TagsController()
        {
        }

        public TagsController(IDbConnection db) : base(db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Select<Tag>());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Tag request)
        {
            _db.Insert(request);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(_db.GetById<Tag>(id));
        }

        [HttpPost]
        public ActionResult Edit(Tag tag)
        {
            _db.Update(tag);
            return View(tag);
        }

        public ActionResult Delete(int id)
        {
            return View(_db.GetById<Tag>(id));
        }

        [HttpPost]
        public ActionResult Delete(Tag request)
        {
            _db.Delete<Tag>(request);
            return RedirectToAction("Index");
        }
    }
}
