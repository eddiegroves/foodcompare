using FoodCompare.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodCompare.Web.Controllers
{
    public class TaggedController : Controller
    {
        public ActionResult Index(Tag request)
        {
            return View(request);
        }
    }
}
