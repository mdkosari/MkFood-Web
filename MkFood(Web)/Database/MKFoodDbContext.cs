
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
        /*public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Bill> Bills { get; set; }*/


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Food>().ToTable("Foods");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Price>().ToTable("Prices");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Bill>().ToTable("Bills");*/

            /*modelBuilder.ApplyConfiguration(new CategoryConfiguration())
                .ApplyConfiguration(new FoodConfiguration())
                .ApplyConfiguration(new PriceConfiguration())
                .ApplyConfiguration(new CustomerConfiguration())
                .ApplyConfiguration(new OrderConfiguration())
                .ApplyConfiguration(new BillConfiguration());*/

            modelBuilder.Entity<Category>().HasMany(c => c.Foods).WithOne(f => f.Category);
            modelBuilder.Entity<Price>().HasOne(p => p.Food).WithMany(f => f.Prices);
        }


        // Shadow Properties
        /*public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            //Food
            foreach (var item in ChangeTracker.Entries<Food>()
            .Where(e => e.State == EntityState.Added
            || e.State == EntityState.Modified))
            {
                item.CurrentValues["LastUpdated"] = DateTime.Now;
                
            }

            //Category
            foreach (var item in ChangeTracker.Entries<Category>()
            .Where(e => e.State == EntityState.Added
            || e.State == EntityState.Modified))
            {
                item.CurrentValues["LastUpdated"] = DateTime.Now;

            }

            //Bill
            foreach (var item in ChangeTracker.Entries<Bill>()
            .Where(e => e.State == EntityState.Added
            || e.State == EntityState.Modified))
            {
                item.CurrentValues["LastUpdated"] = DateTime.Now;

            }

            //Price
            foreach (var item in ChangeTracker.Entries<Price>()
            .Where(e => e.State == EntityState.Added
            || e.State == EntityState.Modified))
            {
                item.CurrentValues["LastUpdated"] = DateTime.Now;

            }
            return base.SaveChangesAsync(cancellationToken);
        }*/
    }
}
