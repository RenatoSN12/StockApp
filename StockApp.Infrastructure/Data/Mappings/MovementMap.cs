using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Domain.Entities;

namespace StockApp.Infrastructure.Data.Mappings;

public class MovementMap : IEntityTypeConfiguration<Movement>
{
    public void Configure(EntityTypeBuilder<Movement> builder)
    {
        builder.ToTable("Movement");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x=> x.Quantity)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.MovementType)
            .HasColumnType("SMALLINT")
            .HasConversion<int>()
            .IsRequired();
        
        builder.Property(x => x.Status)
            .HasColumnType("SMALLINT")
            .HasConversion<int>()
            .IsRequired();
        
        builder.Property(x=> x.Description)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(500);
        
        builder.HasOne(x=> x.Product)
            .WithMany()
            .HasForeignKey(x=> x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x=> x.OriginLocation)
            .WithMany()
            .HasForeignKey(x=> x.OriginLocationId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x=> x.DestinationLocation)
            .WithMany()
            .HasForeignKey(x=> x.DestinationLocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}