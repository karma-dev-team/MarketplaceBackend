using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using KarmaMarketplace.Domain.Market.Entities;

namespace KarmaMarketplace.Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Description)
                .HasMaxLength(2048)
                .IsRequired(); 
            builder
                .Property(e => e.Status)
                .HasConversion<string>();
        }
    }
}
