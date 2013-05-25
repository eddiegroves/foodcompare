using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodCompare.Web.Models
{
    public class Tag
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}