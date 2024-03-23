using KarmaMarketplace.Domain.User.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using KarmaMarketplace.Domain.Payment.Entities;

namespace KarmaMarketplace.Infrastructure.Data.Configuration
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<PurchaseEntity>
    {
        public void Configure(EntityTypeBuilder<PurchaseEntity> builder)
        {
            builder
                .Property(e => e.Status)
                .HasConversion<string>();
            builder
                .Property(e => e.Currency)
                .HasConversion<string>();
        }
    }
}
