using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CsvHelper;
using ServiceStack.OrmLite;
using FoodCompare.Web.Models;

namespace FoodCompare.Web.Controllers
{
    public class ImportController : BaseController
    {
        private readonly IDbConnection _db;

        public ImportController(IDbConnection db) : base(db)
        {
            _db = db;
        }

        public ActionResult Tags(HttpPostedFileBase file)
        {
            Import<Tag>(file);
            return RedirectToAction("Index", "Tags");
        }

        public ActionResult Foods(HttpPostedFileBase file)
        {
            Import<Food>(file);
            return RedirectToAction("Index", "Foods");
        }

        public ActionResult Scraps(HttpPostedFileBase file)
        {
            Import<Scrap>(file);
            return RedirectToAction("Index", "Scraps");
        }

        private void Import<T>(HttpPostedFileBase file) where T : class, new()
        {
            using (var reader = new StreamReader(file.InputStream))
            {
                new DbCsvImporter(_db).Read<T>(reader);
            }
        }
    }

    public class DbCsvImporter
    {
        private readonly IDbConnection _db;

        public DbCsvImporter(IDbConnection db)
        {
            _db = db;
        }

        public void Read<T>(TextReader reader) where T : class, new()
        {
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<T>().ToList();
                _db.InsertAll<T>(records);
            }
        }
    }
}
