
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using System.Diagnostics;

namespace MKFood.DB.Models
{

    public class MKFoodDbContext : DbContext
    {
        public MKFoodDbContext(DbContextOptions<MKFoodDbContext> options) : base(options) { }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Bill> Bills { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Category
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Foods)
                .WithOne(f => f.Category);

            //Price
            modelBuilder.Entity<Price>()
                .HasOne(p => p.Food)
                .WithMany(f => f.Prices);
            //Bills
            modelBuilder.Entity<Bill>()
                .HasMany(b => b.orders)
                .WithOne(o => o.Bill);
        }
    }
}
