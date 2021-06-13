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
        private readonly string _connectionString = "Data Source=.;Database=CodecoolTravel;Integrated Security=true";

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }

        public ShopContext()
        {
        }
        
        
        /*
        public ShopContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ShopDb");
        }
        */
        
        public DbSet<Product> Product { get; set; }
        public DbSet<AddressData> AddressData { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItems{ get; set; }
        public DbSet<Country> Country { get; set; }


        public DbSet<ProductCategory> ProductCategory { get; set; }

        public DbSet<OrderData> OrderData { get; set; }

        public DbSet<Status> OrderStatus { get; set; }
        public DbSet<TravelAgency> TravelAgency { get; set; }
        public DbSet<UserData> User { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>()
                .HasMany(usr => usr.AddressData)
                .WithOne(adr => adr.User)
                .HasForeignKey(adr => adr.UserId);
            modelBuilder.Entity<Cart>()
                .HasMany(cart => cart.CartItems)
                .WithOne();
                

            modelBuilder.Entity<CartItem>()
                .HasOne(cart => cart.Product)
                .WithOne();
            base.OnModelCreating(modelBuilder);
        }

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