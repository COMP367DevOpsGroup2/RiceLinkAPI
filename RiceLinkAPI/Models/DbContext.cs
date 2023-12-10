using Microsoft.EntityFrameworkCore;
using RiceLinkAPI.Models.Customer;
using RiceLinkAPI.Models.Orders;
using RiceLinkAPI.Models.Products;
using System;

namespace RiceLinkAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
            Products = Set<Product>();
            CustomerModel = Set<CustomerModel>();
            Orders = Set<Order>();
            OrderItem = Set<OrderItem>();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerModel> CustomerModel { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)"); // precision and scale
                                                  // configuration for Order.TotalPrice
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18, 2)"); // precision and scale for Order.TotalPrice

            // configuration for OrderItem.UnitPrice
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18, 2)"); // precision and scale for OrderItem.UnitPrice
        }


    }



}

