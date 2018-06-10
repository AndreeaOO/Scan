using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using System.Text;

namespace Scan
{
    public class DataService
    {
       //static Task mainTask = new Task(new Action(Main));
        
        
        //126.3 

    //Add download of file if it does not exisit?
        static void Main(string[] args)
        {

            Task.Run(async () => await StartDbxClient());
            //var testfile = File.Open("test_file.txt", FileMode.Open);
            DropboxClient dbxc = new DropboxClient("kvV3uLWjyqAAAAAAAAAAB_rUpZkAJqx9zaVe2PztBX60BpxjvRHRuzH2p_i6A3PE");
            /*Task.Run(async () => await UploadToDB(dbxc,"Smart_fridge", "test.txt", testfile.ToString()));
            Console.WriteLine("FIle ", testfile, "uploloaded");*/
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
                    // If Product does not exisit in DB, create it and ad it to the list of items in the fridge.    
                    var product = service.GetProduct(double.Parse(line));
                    if (product == null)
                    {
                        Console.WriteLine("The product does not exist in the database, please insert product name");
                        string name = Console.ReadLine();
                        var new_product = service.CreateProduct(double.Parse(line), name);
                        Console.WriteLine("product "+name+" created");
                        var new_product_list = service.CreateProduct_List(name);
                        var outputFile = "in_the_fridge.txt";
                        AppendTextList(outputFile, name);
                        var fileStream = File.Open("in_the_fridge.txt", FileMode.Open);
                        try
                        {
                            Task.Run(async () => await UploadToDB(dbxc, "Smart_fridge", "in_the_fridge.txt", fileStream.ToString()));
                            Console.WriteLine("FIle " + fileStream.Name + "uploaded");
                        }
                        catch (DropboxException e)
                        { 
                            Console.WriteLine("An error has occourred");
                        }
                    }
                    else Console.WriteLine(product.Name);


                    var productList = service.GetProductList(product.Name);
                    var product_buy = service.GetProductBuy(product.Name);
                    if (productList == null)
                    {
                        if (product_buy == null)
                        {
                            // Add product to the list of items in the fridge if it exisits in DB already
                            var list = service.CreateProduct_List(product.Name);
                            Console.WriteLine("Product in the fridge");
                            db.SaveChanges();
                            var outputFile = "in_the_fridge.txt";

                            AppendTextList(outputFile, product.Name);
                            var fileStream = File.Open("in_the_fridge.txt", FileMode.Open);   
                            Task.Run(async () => await UploadToDB(dbxc,"Smart_fridge", "in_the_fridge.txt", fileStream.ToString()));
                            Console.WriteLine("FIle ", outputFile, " uploaded");        
                        }
                        else
                        {
                            //Else if it exists in the to_buy list, remove it from there and put it back in the fridge list
                            Console.WriteLine("Product removed from the to buy list and added to the fridge list");
                            var list = service.CreateProduct_List(product.Name);
                            db.To_Buy.Remove(product_buy);
                            db.SaveChanges();
                            var outputFile1 = "to_buy.txt";

                            RemoveFromFileBuy(outputFile1, product.Name);

                            var outputFile = "in_the_fridge.txt";

                            AppendTextList(outputFile, product.Name);
                            
                        }
                    }
                    else
                    {
                        //If product already exisits in the fridge list, remove it and place it in the to buy list
                        db.List.Remove(productList);
                        Console.WriteLine("Product removed from fridge list");
                        var buy = service.CreateProduct_Buy(product.Name);
                        Console.WriteLine("Product added to the to buy list");
                        db.SaveChanges();
                        var outputFile = "to_buy.txt";

                        AppendTextBuy(outputFile, product.Name);
                        
                        var outputFile1 = "in_the_fridge.txt";

                        RemoveFromFileList(outputFile1, product.Name);

                    }
                }
            } while ((line = Console.ReadLine()) != null || line.ToLower() != "done");


            var input = line.ToString();
            if (input.ToLower() == "done")
            {
                
            }


        } // End of Main()
        
        //Below are all the DataService methods used to communicate with the database.

        public List<Products> GetProducts() //Returns a list of products present in the DB
        {
            var db = new ScanContext();
            return db.Products.ToList<Products>();
        }

        public List<List> GetList() //Returns a list of items in the fridge
        {
            var db = new ScanContext();
            return db.List.ToList<List>();
        }

        public List<To_Buy> GetBuy() //Returns a list of products to buy
        {
            var db = new ScanContext();
            return db.To_Buy.ToList<To_Buy>();
        }

        public Products GetProduct(double code) //Find product in DB and return it.
        {
            using (var db = new ScanContext())
            {
                var product = db.Products.Find(code);
                if (product == null) return null;
                return product;
            }
        }

        public List GetProductList(string name) //Find product in the list of items in the fridge and return it.
        {
            using (var db = new ScanContext())
            {
                var product = db.List.Find(name);
                if (product == null) return null;
                return product;
            }
        }


        public To_Buy GetProductBuy(string name) //Find a Product in the list of items to buy and return it.
        {
            using (var db = new ScanContext())
            {
                var product = db.To_Buy.Find(name);
                if (product == null) return null;
                return product;
            }
        }

        public Products CreateProduct(double code, string name) //Create a new Porduct in the DB.
        {
            using (var db = new ScanContext())
            {
                var product = new Products { Code = code, Name = name };
                db.Products.Add(product);
                db.SaveChanges();
                return product;
            }
        }

        public List CreateProduct_List(string name) //Create a new item in the list of stuff in the fridge.
        {
            using (var db = new ScanContext())
            {
                var list = new List { Name = name };
                db.List.Add(list);
                db.SaveChanges();
                return list;
            }
        }

        public To_Buy CreateProduct_Buy(string name) //Create new entry in the list of things to buy.
        {
            using (var db = new ScanContext())
            {
                var buy = new To_Buy { Name = name };
                db.To_Buy.Add(buy);
                db.SaveChanges();
                return buy;
            }
        }


        public bool UpdateProduct(double code, string Name) //Change name of a Product in the DB.
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

        public bool DeleteProduct(double code) // Remove a Product from the DB.
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

        public bool DeleteProduct_List(string name) //Remove a product from the list of stuff in the fridge
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

        public bool DeleteProduct_Buy(string name) //Remove a product from the list of things to buy
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


        static async Task StartDbxClient() { //Initialize the DropBox client using a static API key
            var dbx = new DropboxClient("kvV3uLWjyqAAAAAAAAAAB_rUpZkAJqx9zaVe2PztBX60BpxjvRHRuzH2p_i6A3PE");
                var full = await dbx.Users.GetCurrentAccountAsync();
                Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
        }
        
        public static void WriteWordsToFile(string outputFile, string words) //Append a string to a file (used to update the local text files, but it does the same thing as the method below and is not used afaik) 
        {
            using (var writer = new StreamWriter(File.OpenWrite(outputFile)))
            {

                writer.WriteLine($"{words}");

            }
        }

        public static void AppendTextList(string outputFile, string words) 
        {

            using (var writer = new StreamWriter(outputFile, true))
            {
                
                writer.WriteLine($"{words}");
                writer.Close();

            }

        }
        static async Task UploadToDB(DropboxClient dbx, string folder, string file, string content) { //Upload a file to DropBox, in the specified path
            using (var mem = new MemoryStream(Encoding.UTF8.GetBytes(content))) {
                var updated = await dbx.Files.UploadAsync(
                    folder + "/" + file,
                    WriteMode.Overwrite.Instance,
                    body: mem);
                Console.WriteLine("Saved {0}/{1} rev {2}", folder, file, updated.Rev);
            }
        }

        public static void AppendTextBuy(string outputFile, string words) //Another method to append text to files, pretty sure this is only here because there used to be a static path
        {
            //var filePath = "C:\Users\Andreea\Dropbox\in_the_fridge.txt";
            using (var writer = new StreamWriter(outputFile, true))
            {

                writer.WriteLine($"{words}");
                writer.Close();

            }

        }


        public static void RemoveFromFileList(string outputFile, string words) //Search and remove an item from the local List file (contains products in the fridge). This is pretty clever :D
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

        public static void RemoveFromFileBuy(string outputFile, string words) //Same as above but for the list of things to buy.
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
