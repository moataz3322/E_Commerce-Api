using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.InfraStructure.Data.Configs
{
    class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            //Order And OrderItem  htb2a 1 msh many   with one mn n7yt al Item
            builder.HasMany(o => o.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
                
            builder.Property(x=>x.SubTotal).HasColumnType("decimal(8,2)");

            //owned entity
            builder.OwnsOne(o => o.ShipToAdress, adress =>
            {
                //dol al prop al gwa al Adress b3mlha configuration 3shan al efCore msh by3rfha automatically
                adress.Property(a => a.FirstName).HasMaxLength(50);
                adress.Property(a => a.LastName).HasMaxLength(50);
                adress.Property(a => a.City).HasMaxLength(50);
                adress.Property(a => a.Street).HasMaxLength(50);
                adress.Property(a => a.Country).HasMaxLength(50);

            });
            //de 34na a7wleha l String nvarchar
            builder.Property(x => x.OrderStatus).HasConversion<string>().HasMaxLength(50);
            

        }
    }
}
