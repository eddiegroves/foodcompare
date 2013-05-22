using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodCompare.Web.Controllers
{
    public class FoodsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
