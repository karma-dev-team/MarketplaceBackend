using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NameApp.Domain.User.Entities;

namespace NameApp.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(t => t.UserName)
                .HasMaxLength(200)
                .IsRequired();
        }   
    }
}
