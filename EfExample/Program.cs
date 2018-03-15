using System;

namespace EfExample
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var db = new NorthwindContext())
            {
                foreach (var category in db.Categories)
                {
                    Console.WriteLine(category.Name);
                }
            }
        }
    }
}
