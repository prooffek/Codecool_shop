using System;
using Codecool.CodecoolShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Codecool.CodecoolShop.Daos
{
    public class ShopContext : DbContext
    {
        private const string ConnectionString = "Data Source=.;Database=CodecoolTravel;Integrated Security=true";

        public DbSet<Product> Product { get; set; }
        public DbSet<Status> OrderStatus { get; set; }
        public DbSet<TravelAgency> TravelAgency { get; set; }
        public DbSet<UserData> User { get; set; }

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
                .UseSqlServer(ConnectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
    }
}