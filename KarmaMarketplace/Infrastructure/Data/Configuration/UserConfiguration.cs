using KarmaMarketplace.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.User.Enums;


namespace KarmaMarketplace.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(t => t.UserName)
                .HasMaxLength(200)
                .IsRequired();
            builder
                .Property(e => e.Role)
                .HasConversion<string>();
        }   
    }
}
