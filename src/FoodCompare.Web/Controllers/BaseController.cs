using System;
using System.Data;
using System.Web.Mvc;
using FoodCompare.Web.Models;
using ServiceStack.OrmLite;

namespace FoodCompare.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IDbConnection _db;

        public BaseController(IDbConnection db)
        {
            _db = db;
            PopulateNavigationViewData();
        }

        public void PopulateNavigationViewData()
        {
            try
            {
                ViewBag.Tags = _db.Select<Tag>();
            }
            catch
            { 
                // Empty catch for the time being
            }
        }
    }
}