using KarmaMarketplace.Domain.User.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using KarmaMarketplace.Domain.Market.Entities;

namespace KarmaMarketplace.Infrastructure.Data.Configuration
{
    public class OptionConfiguration : IEntityTypeConfiguration<OptionEntity>
    {
        public void Configure(EntityTypeBuilder<OptionEntity> builder)
        {
            builder
                .Property(e => e.Type)
                .HasConversion<string>();
        }
    }
}
