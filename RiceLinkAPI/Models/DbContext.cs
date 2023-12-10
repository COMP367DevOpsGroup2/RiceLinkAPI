using Microsoft.EntityFrameworkCore;
using RiceLinkAPI.Models.Customer;
using RiceLinkAPI.Models.Products;
using System;

namespace RiceLinkAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerModel> CustomerModel { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)"); // precision and scale


        }



    }
}
