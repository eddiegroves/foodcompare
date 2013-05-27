using CsvHelper;
using FoodCompare.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;

namespace FoodCompare.Web.Controllers
{
    public class ExportController : BaseController
    {
        private readonly IDbConnection _db;

        public ExportController(IDbConnection db) : base(db)
        {
            _db = db;
        }

        public ActionResult Tags(bool text = false)
        {
            Export<Tag>(text);
            return new EmptyResult();
        }

        public ActionResult Foods(bool text = false)
        {
            Export<Food>(text);
            return new EmptyResult();
        }

        private void Export<T>(bool text) where T: class
        {
            new DbCsvExporter(_db).Write<T>(new StreamWriter(Response.OutputStream));

            if (text)
            {
                Response.ContentType = "text/plain";
            }
            else
            {
                Response.ContentType = "text/csv";
                string fileName = typeof(T).Name.ToLower();
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}.csv", fileName));
            }
        }
    }

    public class DbCsvExporter
    {
        private readonly IDbConnection _db;

        public DbCsvExporter(IDbConnection db)
        {
            _db = db;
        }

        public void Write<T>(TextWriter writer) where T : class
        {
            var table = _db.Select<T>();
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(table);
            }
        }
    }
}
