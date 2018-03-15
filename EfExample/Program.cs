using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EfExample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NorthwindContext())
            {
                foreach (var product in db.Products.Include(x => x.Category))
                {
                    Console.WriteLine($"{product.Name} {product.Category.Name}");
                }
            }
            //UpdateCategoryName(5, "Updated");
            //CreateCategory(new Category { Name = "Test", Description = "Test" });
            //SelectCategories();
        }


        static void UpdateCategoryName(int id, string name)
        {
            using (var db = new NorthwindContext())
            {
                var category = db.Categories.FirstOrDefault(x => x.Id == id);
                if (category == null) return;
                category.Name = name;
                db.SaveChanges();
            }
        }

        static void CreateCategory(Category category)
        {
            using (var db = new NorthwindContext())
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }

        private static void SelectCategories()
        {
            using (var db = new NorthwindContext())
            {
                foreach (var category in db.Categories)
                {
                    Console.WriteLine(category.Name);
                }
            }
        }
    }
}
