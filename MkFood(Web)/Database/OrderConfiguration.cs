using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFood.DB.Models
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders").HasKey(o => o.OrderId);

            builder.Property(o => o.OrderId).ValueGeneratedOnAdd();

            builder.HasOne(o => o.Food);

            builder.HasOne(o => o.Bill).WithMany(b => b.orders).HasForeignKey("BiilId");


        }
    }
}
