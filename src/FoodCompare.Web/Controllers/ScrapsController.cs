using FoodCompare.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Net;

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
            return View(new Scrap());
        }

        public ActionResult AddFromScrap(int id)
        {
            var scrap = _db.GetById<Scrap>(id);
            scrap.Url = String.Empty;
            return View("Add", scrap);
        }

        [HttpPost]
        public ActionResult Add(Scrap scrap)
        {
            _db.Insert(scrap);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
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
            ViewBag.Errors = String.Empty;
            ScrappedContent scrappedContent = new ScrappedContent();

            try
            {
                string content = String.Empty;
                using (var client = new WebClient())
                {
                    content = client.DownloadString(scrap.Url);
                }

                var scrapper = new XPathScrapper();
                scrappedContent = scrapper.Scrap(scrap, content);
            }
            catch (Exception ex)
            {
                ViewBag.Errors = ex.ToString();
            }

            ViewBag.Id = scrap.Id;
            scrap.Preview = scrappedContent.Food;
            _db.Update(scrap);
            return View(scrappedContent);
        }
    }

    public class ScrappedContent
    {
        public string Messages { get; set; }
        public Food Food { get; set; }
    }

    public class XPathScrapper
    {
        private StringBuilder _log = new StringBuilder();

        public ScrappedContent Scrap(Scrap scrap, string content)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(content);

            var food = new Food();

            if (!String.IsNullOrEmpty(scrap.BrandXPath))
            {
                Log("Brand");
                food.Brand = GetString(doc, scrap.BrandXPath);
            }

            if (!String.IsNullOrEmpty(scrap.ProductXPath))
            {
                Log("Product");
                food.Product = GetString(doc, scrap.ProductXPath);
            }

            if (!String.IsNullOrEmpty(scrap.CaloriesXPath))
            {
                Log("Calories");
                food.Calories = GetDecimal(doc, scrap.CaloriesXPath);
            }

            if (!String.IsNullOrEmpty(scrap.CarbohydrateXPath))
            {
                Log("Carbohydrate");
                food.Carbohydrate = GetDecimal(doc, scrap.CarbohydrateXPath);
            }

            if (!String.IsNullOrEmpty(scrap.ProteinXPath))
            {
                Log("Protein");
                food.Protein = GetDecimal(doc, scrap.ProteinXPath);
            }

            if (!String.IsNullOrEmpty(scrap.FatXPath))
            {
                Log("Fat");
                food.Fat = GetDecimal(doc, scrap.FatXPath);
            }

            return new ScrappedContent
            {
                Food = food,
                Messages = _log.ToString()
            };
        }

        private string GetString(HtmlDocument doc, string xpath)
        {
            var node = doc.DocumentNode.SelectSingleNode(xpath);
            if (node == null)
            {
                Log("Could not find node with XPath: {0}", xpath);
                return String.Empty;
            }

            string text = node.InnerText;
            if (String.IsNullOrWhiteSpace(text))
            {
                Log("No text found with XPath: {0}", xpath);
                return String.Empty;
            }

            Log("Result: {0}", text);
            return text;
        }

        private decimal GetDecimal(HtmlDocument doc, string xpath)
        {
            var node = doc.DocumentNode.SelectSingleNode(xpath);
            if (node == null)
            {
                Log("Could not find node with XPath: {0}", xpath);
                return 0M;
            }

            string text = node.InnerText;
            if (String.IsNullOrWhiteSpace(text))
            {
                Log("No text found with XPath: {0}", xpath);
                return 0M;
            }

            Log("Parsing text: {0}", text);
            text = text.ToLower();
            decimal value = 0M;
            string originalText = text;

            // Remove any non digit characters
            text = Regex.Replace(text, "[^0-9.]", String.Empty);

            // Convert to decimal
            if (!Decimal.TryParse(text, out value))
            {
                Log("Could not parse {0} into a decimal", text);
            }

            // KJ => Calories
            if (originalText.Contains("kj"))
            {
                Log("Detected kilo joules, converting to calories");
                value = Math.Round(value / 4.2M, 0);
            }

            // <1.0 => 0
            if (originalText.Contains("&lt;"))
            {
                Log("Converting <1.0 to 0");
                value = 0M;
            }

            Log("Result: {0}", value);
            return value;
        }

        private void Log(string message)
        {
            _log.AppendLine(message);
        }

        private void Log(string message, params object[] args)
        {
            _log.AppendFormat(message, args).AppendLine(); ;
        }
    }
}
