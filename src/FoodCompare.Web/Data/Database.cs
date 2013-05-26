using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using FoodCompare.Web.Models;

namespace FoodCompare.Web.Data
{
    public class Database
    {
        private const string connectionString =
            @"Data Source=(localdb)\v11.0;Integrated Security=True;Initial Catalog=FoodCompare";

        public static IDbConnectionFactory Factory
        {
            get { return new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider); }
        }

        // Creates FoodCompare database schema
        public static void Create()
        {
            using (var db = Database.Factory.OpenDbConnection())
            {
                db.DropAndCreateTable<Food>();
                db.DropAndCreateTable<Tag>();
            }
        }

        // Seeds FoodCompare database
        public static void Seed()
        {
            using (var db = Database.Factory.OpenDbConnection())
            {
                db.Insert(new Food
                {
                    Brand = "John West",
                    Product = "Tuna in Springwater",
                    Calories = "130",
                    Carbohydrate = "0",
                    Protein = "26.5",
                    Fat = "1.2",
                    Tags = "Tag One"
                });

                db.Insert(new Food
                {
                    Brand = "John West",
                    Product = "Tuna in Olive Oil",
                    Calories = "160",
                    Carbohydrate = "0",
                    Protein = "26.5",
                    Fat = "6.2",
                    Tags = "Tag One, Tag3"
                });

                db.InsertAll(new [] {
                    new Tag { Name = "Tag One" },
                    new Tag { Name = "Tag-2" },
                    new Tag { Name = "Tag3" }
                });
            }
        }
    }
}