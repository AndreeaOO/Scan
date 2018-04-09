using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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

            Console.WriteLine("Start scanning");
            string line = Console.ReadLine();
            do
            {
                using (var db = new ScanContext())
                {

                    var service = new DataService();
                    var product = service.GetProduct(double.Parse(line));
                    if (product == null)
                    {
                        Console.WriteLine("The product does not exist in the database, please insert product name");
                        string name = Console.ReadLine();
                        var new_product = service.CreateProduct(double.Parse(line), name);
                        Console.WriteLine("product "+name+" created");
                        var new_product_list = service.CreateProduct_List(name);
                        var outputFile = "in_the_fridge.txt";

                        //File.OpenText(outputFile);
                        AppendText(outputFile, name);
                        
                    }
                    else Console.WriteLine(product.Name);


                    var product_list = service.GetProductList(product.Name);
                    var product_buy = service.GetProductBuy(product.Name);
                    if (product_list == null)
                    {
                        if (product_buy == null)
                        {
                            var list = service.CreateProduct_List(product.Name);
                            Console.WriteLine("Product in the fridge");
                            db.SaveChanges();
                            var outputFile = "in_the_fridge.txt";

                            //File.OpenText(outputFile);
                            AppendText(outputFile, product.Name);
                        }
                        else
                        {
                            Console.WriteLine("Product removed from the to buy list and added to the fridge list");
                            var list = service.CreateProduct_List(product.Name);
                            db.To_Buy.Remove(product_buy);
                            db.SaveChanges();
                            var outputFile1 = "to_buy.txt";

                            //File.OpenText(outputFile1);
                            RemoveFromFile(outputFile1, product.Name);

                            var outputFile = "in_the_fridge.txt";

                            //File.OpenText(outputFile);
                            AppendText(outputFile, product.Name);
                        }
                    }
                    else
                    {

                        db.List.Remove(product_list);
                        Console.WriteLine("Product removed from fridge list");
                        var buy = service.CreateProduct_Buy(product.Name);
                        Console.WriteLine("Product added to the to buy list");
                        db.SaveChanges();
                        var outputFile = "to_buy.txt";

                        //File.OpenText(outputFile);
                        AppendText(outputFile, product.Name);
                        
                        var outputFile1 = "in_the_fridge.txt";

                        //File.OpenText(outputFile1);
                        RemoveFromFile(outputFile1, product.Name);

                    }
                }
            } while ((line = Console.ReadLine()) != null || line.ToLower() != "done");


            var input = line.ToString();
            if (input.ToLower() == "done")
            {
                
            }
            

            //var service = new DataService();
            //var delete = service.DeleteProduct_List("test1");
            //var create = service.CreateProduct(1, "test5");
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


        public static void AppendText(string outputFile, string words)
        {

            using (var writer = new StreamWriter(outputFile, true))
            {
                
                writer.WriteLine($"{words}");
                writer.Close();

            }

        }


        public static void RemoveFromFile(string outputFile, string words)
        {
            string search_text = words;
            string old;
            string n = "";
            StreamReader sr = File.OpenText(outputFile);
            while ((old = sr.ReadLine()) != null)
            {
                if (!old.Contains(search_text))
                {
                    n += old + Environment.NewLine;
                }
            }
            sr.Close();
            File.WriteAllText(outputFile, n);
        }
    }
}
