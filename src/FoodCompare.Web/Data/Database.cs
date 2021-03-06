﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using FoodCompare.Web.Models;
using System.Configuration;

namespace FoodCompare.Web.Data
{
    public class Database
    {
        private const string connectionString =
            @"Data Source=(localdb)\v11.0;Integrated Security=True;Initial Catalog=FoodCompare";

        public static string ConnectionString
        {
            get
            {
                if (ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"] != null)
                    return ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"];
                if (ConfigurationManager.ConnectionStrings["SQLSERVER_CONNECTION_STRING"] != null)
                    return ConfigurationManager.ConnectionStrings["SQLSERVER_CONNECTION_STRING"].ConnectionString;
                else return connectionString;
            }
        }

        public static IDbConnectionFactory Factory
        {
            get { return new OrmLiteConnectionFactory(ConnectionString, SqlServerDialect.Provider); }
        }

        // Creates FoodCompare database schema
        public static void Create()
        {
            using (var db = Database.Factory.OpenDbConnection())
            {
                db.DropAndCreateTable<Food>();
                db.DropAndCreateTable<Tag>();
                db.DropAndCreateTable<Scrap>();
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
                    Calories = 130,
                    Carbohydrate = 0,
                    Protein = 26.5M,
                    Fat = 1.2M,
                    Tags = "Tag One"
                });

                db.Insert(new Food
                {
                    Brand = "John West",
                    Product = "Tuna in Olive Oil",
                    Calories = 160,
                    Carbohydrate = 0,
                    Protein = 26.5M,
                    Fat = 6.2M,
                    Tags = "Tag One, Tag3"
                });

                db.InsertAll(new [] {
                    new Tag { Name = "Tag One", ShowTotal = true },
                    new Tag { Name = "Tag-2" },
                    new Tag { Name = "Tag3" }
                });

                db.Insert(new Scrap
                {
                    FoodId = db.First<Food>(p => p.Product.Contains("Springwater")).Id,
                    Url = "http://johnwest.com.au/our-range/tuna/no-drain-tuna/no-drain-tuna-springwater-130g"
                });
            }
        }
    }
}