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
    class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(o => o.Price).HasColumnType("decimal(8,2)");
            builder.OwnsOne(o => o.Product, product=>{ 
            
                product.Property(p => p.ProductName).HasMaxLength(100);
                product.Property(p => p.PictureUrl).HasMaxLength(200);


            });
        }
    }
}
