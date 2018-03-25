using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Scan
{
    public class DataService
    {
        static void Main(string[] args)
        {
            /*using (var db = new ScanContext())
            {
                foreach (var p in db.Products)
                {
                    Console.WriteLine($"{p.Name} {p.Code}");
                }
            }*/

            using (var db = new ScanContext())
            {
                var input = Console.ReadLine();
                var service = new DataService();
                var product = service.GetProduct(double.Parse(input));
                if (product == null)
                {
                    Console.WriteLine("The product does not exist");
                }
                else Console.WriteLine(product.Name);

                var product_list = service.GetProductList(product.Name);
                if (product_list == null)
                {
                    var list = service.CreateProduct_List(product.Name);
                }
                else db.List.Remove(product_list);
                Console.WriteLine("Product removed");
                db.SaveChanges();
            }

            //var delete = service.DeleteProduct_List("option4");
            //var update = service.UpdateProduct(12345, "test4");


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

        public List GetProductList(string name)
        {
            using (var db = new ScanContext())
            {
                var product = db.List.Find(name);
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




        //if inputcode == product_code add product_name to list
        
    }
}
