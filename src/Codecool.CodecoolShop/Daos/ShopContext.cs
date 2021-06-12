using System;
using System.Data;
using Codecool.CodecoolShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Codecool.CodecoolShop.Daos
{
    public class ShopContext : DbContext
    {
        private readonly string _connectionString; 
        public ShopContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ShopDb");
        }
        
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<OrderData> OrderDatas { get; set; }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>();
            base.OnModelCreating(modelBuilder);
        }
        */
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
    }
}