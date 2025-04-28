using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Domain.Entities;

namespace StockApp.Infrastructure.Data.Mappings;

public class ProductStockMap : IEntityTypeConfiguration<ProductStock>
{
    public void Configure(EntityTypeBuilder<ProductStock> builder)
    {
        builder.ToTable("ProductStock");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.MinimumStockLevel).HasColumnType("INT");
        builder.Property(x => x.MaximumStockLevel).HasColumnType("INT");
        builder.Property(x => x.Quantity).HasColumnType("INT");
        builder.Property(x=> x.LastUpdatedDate).IsRequired().HasColumnType("DATETIME2");
        builder.Property(x => x.UserId).IsRequired().HasColumnType("VARCHAR").HasMaxLength(80);

        builder.HasOne(x => x.Product).WithMany(i => i.Inventories).HasForeignKey(x => x.ProductId);
        builder.HasOne(x=> x.Location).WithMany().HasForeignKey(i => i.LocationId);;
        
        builder.HasIndex(x=> new {x.LocationId, x.ProductId}).IsUnique();
    }
}