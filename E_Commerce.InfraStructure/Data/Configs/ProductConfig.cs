using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.InfraStructure.Data.Configs
{
    class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand)
                .WithMany().HasForeignKey(s => s.BrandId);
            builder.HasOne(p => p.ProductType)
                .WithMany().HasForeignKey(s => s.TypeId);

            builder.Property(p => p.Price).HasColumnType("Decimal(18,2)");
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.PictureUrl).HasMaxLength(200);

        }
    }
}
