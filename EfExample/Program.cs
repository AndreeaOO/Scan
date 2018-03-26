using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

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
                    Console.WriteLine("Product in the fridge");
                    db.SaveChanges();
                    var outputFile = "in_the_fridge.txt";
                    WriteWordsToFile(outputFile, product.Name);
                }
                else
                {
                    db.List.Remove(product_list);
                    Console.WriteLine("Product removed");
                    var buy = service.CreateProduct_Buy(product.Name);
                    Console.WriteLine("To buy");
                    db.SaveChanges();
                    var outputFile = "to_buy.txt";
                    WriteWordsToFile(outputFile, product.Name);
                    var outputFile1 = "in_the_fridge.txt";
                    RemoveFromFile (outputFile1, product.Name);

                }

            }

            /*var service = new DataService();
            var delete = service.DeleteProduct_Buy("test1");*/


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

        public List<To_Buy> GetBuy()
        {
            var db = new ScanContext();
            return db.To_Buy.ToList<To_Buy>();
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


        public To_Buy GetProductBuy(string name)
        {
            using (var db = new ScanContext())
            {
                var product = db.To_Buy.Find(name);
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

        public To_Buy CreateProduct_Buy(string name)
        {
            using (var db = new ScanContext())
            {
                var buy = new To_Buy { Name = name };
                db.To_Buy.Add(buy);
                db.SaveChanges();
                return buy;
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

        public bool DeleteProduct_Buy(string name)
        {
            using (var db = new ScanContext())
            {
                var buy = db.To_Buy.Find(name);
                if (buy == null) return false;
                else
                {
                    db.To_Buy.Remove(buy);
                    db.SaveChanges();
                    return true;
                }
            }
        }

        public static void WriteWordsToFile(string outputFile, string words)
        {
            using (var writer = new StreamWriter(File.OpenWrite(outputFile)))
            {
               
                writer.WriteLine($"{words}");      
                
            }
        }

        public static void RemoveFromFile(string outputFile, string words)
        {
           /* using (var writer = new StreamWriter(File.OpenWrite(outputFile)))
            {

                writer.RemoveLine(words);

            }*/

            string search_text = words;
            string old;
            string n = "";
            StreamReader sr = File.OpenText("in_the_fridge.txt");
            while ((old = sr.ReadLine()) != null)
            {
                if (!old.Contains(search_text))
                {
                    n += old + Environment.NewLine;
                }
            }
            sr.Close();
            File.WriteAllText("in_the_fridge.txt", n);

        }


    }
}
