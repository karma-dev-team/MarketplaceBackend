using KarmaMarketplace.Domain.User.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using KarmaMarketplace.Domain.Messging.Entities;

namespace KarmaMarketplace.Infrastructure.Data.Configuration
{
    public class ChatConfiguration : IEntityTypeConfiguration<ChatEntity>
    {
        public void Configure(EntityTypeBuilder<ChatEntity> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder
                .Property(e => e.Type)
                .HasConversion<string>();
            builder
                .HasMany(e => e.Participants)
                .WithMany(e => e.Chats);
        }
    }
}
