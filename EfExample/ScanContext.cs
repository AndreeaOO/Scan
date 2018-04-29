using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scan 
{
    public class ScanContext : DbContext
    {
        public DbSet<List> List { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<To_Buy> To_Buy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql(
                "server=localhost;database=scan;uid=root;pwd=toor;SslMode=none");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Category>().ToTable("categories");

            modelBuilder.Entity<To_Buy>().Property(x => x.Name).HasColumnName("product_name");

            modelBuilder.Entity<List>().Property(x => x.Name).HasColumnName("product_name");

            modelBuilder.Entity<Products>().Property(x => x.Code).HasColumnName("product_code");
            modelBuilder.Entity<Products>().Property(x => x.Name).HasColumnName("product_name");
        }
    }
}
