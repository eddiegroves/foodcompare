using FoodCompare.Web.Controllers;
using FoodCompare.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace FoodCompare.Web.Tests
{
    public class given_html_content
    {
        [Fact]
        public void it_should_return_food()
        {
            var scrapper = new XPathScrapper();
            var scrap = new Scrap
            {
                CaloriesXPath =  @"//*[@id=""prod-detail""]/section/section[2]/table/tbody/tr[2]/td[3]",
                ProteinXPath = @"//*[@id=""prod-detail""]/section/section[2]/table/tbody/tr[3]/td[3]",
                CarbohydrateXPath = @"//*[@id=""prod-detail""]/section/section[2]/table/tbody/tr[12]/td[3]",
                FatXPath = @"//*[@id=""prod-detail""]/section/section[2]/table/tbody/tr[4]/td[3]",
                ProductXPath = @"//*[@id=""subPageTitle""]"
            };

            var results = scrapper.Scrap(scrap, File.ReadAllText("Data/john-west.html"));
            var expected = new Food
            {
                Brand = "John West",
                Product = "Pole and Line Skipjack Tuna in Springwater 185g",
                Calories = 115,
                Protein = 26.4M,
                Fat = 0.9M,
                Carbohydrate = 0
            };

            Assert.Equal(expected.Calories, results.Food.Calories);
            Assert.Equal(expected.Protein, results.Food.Protein);
            Assert.Equal(expected.Fat, results.Food.Fat);
            Assert.Equal(expected.Carbohydrate, results.Food.Carbohydrate);
            Assert.Equal(expected.Product, results.Food.Product);
        }
    }

    public class JohnWestHtmlScrapper
    {
        public string Domain
        {
            get { return ""; }
        }

        public ScrappedContent Scrap(string content)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(content);

            // cals
            var cal = doc.DocumentNode.SelectSingleNode(@"//*[@id=""prod-detail""]/section/section[2]/table/tbody/tr[2]/td[3]");
            string text = cal.InnerText;
            decimal cals = 0M;

            if (text.ToLower().Contains("kj"))
            {
                cals = Decimal.Parse(text.ToLower().Replace("kj", ""));
                cals = cals / 4.2M;
                // kj 
            }

            // protein
            var pro = doc.DocumentNode.SelectSingleNode(@"//*[@id=""prod-detail""]/section/section[2]/table/tbody/tr[3]/td[3]");
            text = pro.InnerText;
            decimal protein = 0M;

            if (text.ToLower().Contains("g")) text = text.Replace("g", "");
            protein = Decimal.Parse(text);

            // carbs
            var carb = doc.DocumentNode.SelectSingleNode(@"//*[@id=""prod-detail""]/section/section[2]/table/tbody/tr[12]/td[3]");
            text = carb.InnerText;
            decimal carbs = 0M;

            if (text.ToLower().Contains("g")) text = text.Replace("g", "");
            if (text.ToLower().Contains("&lt;1")) text = "0";
            carbs = Decimal.Parse(text);

            // fat
            var fat = doc.DocumentNode.SelectSingleNode(@"//*[@id=""prod-detail""]/section/section[2]/table/tbody/tr[4]/td[3]");
            text = fat.InnerText;
            decimal fats = 0M;

            if (text.ToLower().Contains("g")) text = text.Replace("g", "");
            if (text.ToLower().Contains("+")) text = text.Replace("+", "");
            fats = Decimal.Parse(text);

            // product
            var product = doc.DocumentNode.SelectSingleNode(@"//*[@id=""subPageTitle""]");
            string productText = product.InnerText;

            return new ScrappedContent
            {
                Food =  new Food()
                {
                    Product = productText,
                    Calories = Math.Round(cals, 0),
                    Protein = protein,
                    Carbohydrate = carbs,
                    Fat = fats
                }
            };
        }
    }
}
