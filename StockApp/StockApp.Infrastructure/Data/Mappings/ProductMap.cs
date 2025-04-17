using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Domain.Entities;

namespace StockApp.Infrastructure.Data.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x=> x.CustomId).IsRequired().HasColumnType("VARCHAR").HasMaxLength(20);
        builder.Property(x=> x.Title).IsRequired().HasColumnType("NVARCHAR").HasMaxLength(80);
        builder.Property(x => x.Description).IsRequired(false).HasColumnType("NVARCHAR").HasMaxLength(500);
        builder.Property(x => x.Price).HasColumnType("MONEY");
        builder.Property(x => x.Status).IsRequired().HasColumnType("SMALLINT");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("DATETIME2");
        builder.Property(x => x.UpdatedAt).IsRequired().HasColumnType("DATETIME2");
        builder.Property(x => x.UserId).IsRequired().HasColumnType("VARCHAR").HasMaxLength(80);
        builder.Property(p => p.ImageUrl).HasColumnName("ImageUrl").HasMaxLength(500).IsRequired(false);
        
        builder.HasOne(x => x.Category).WithMany().IsRequired(false);
        builder.HasMany(x => x.Inventories).WithOne(i => i.Product);
        
        builder.HasIndex(x => x.CustomId).IsUnique();
    }
}