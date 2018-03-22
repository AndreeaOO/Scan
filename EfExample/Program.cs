using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace EfExample
{
    public class DataService
    {
        static void Main(string[] args)
        {
            using (var db = new ScanContext())
            {
                foreach (var product in db.Products)
                {
                    Console.WriteLine($"{product.Name} {product.Code}");
                } 
            }
        }

        public List<Products> GetProducts()
        {
            var db = new ScanContext();
            return db.Products.ToList<Products>();
        }

        public List<List> GetList()
        {
            var db = new ScanContext();
            return db.List.ToList<List>();
        }

        public Products GetProduct(double code)
        {
            using (var db = new ScanContext())
            {
                var product = db.Products.Find(code);
                if (product == null) return null;
                return product;
            }
        }

        public Products CreateProduct(double code, string name)
        {
            using (var db = new ScanContext())
            {
                var product = new Products { Code = code, Name = name };
                db.Products.Add(product);
                db.SaveChanges();
                return product;
            }
        }

        public List CreateProduct_List(string name)
        {
            using (var db = new ScanContext())
            {
                var list = new List { Name = name };
                db.List.Add(list);
                db.SaveChanges();
                return list;
            }
        }

        public bool UpdateProduct(double code, string Name)
        {
            using (var db = new ScanContext())
            {
                Products product = db.Products.Find(code);
                if (product == null) return false;
                else
                {
                    product.Name = Name;
                    db.SaveChanges();
                    return true;
                }
            }
        }

        public bool DeleteProduct(double code)
        {
            using (var db = new ScanContext())
            {
                var product = db.Products.Find(code);
                if (product == null) return false;
                else
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                    return true;
                }
            }
        }

        public bool DeleteProduct_List(string name)
        {
            using (var db = new ScanContext())
            {
                var list = db.List.Find(name);
                if (list == null) return false;
                else
                {
                    db.List.Remove(list);
                    db.SaveChanges();
                    return true;
                }
            }
        }
    }
}
