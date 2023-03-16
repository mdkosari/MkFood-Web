using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFood.DB.Models
{
    internal class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bills").HasKey(b => b.BillId);

            builder.Property(b => b.BillId).ValueGeneratedOnAdd();

            builder.HasMany(b => b.orders).WithOne(o => o.Bill);

            //builder.HasOne(b => b.Customer);

            builder.Property<DateTime>("LastUpdated");

        }
    }
}
