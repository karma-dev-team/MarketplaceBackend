using Microsoft.EntityFrameworkCore;
using NameApp.Application.Common.Interfaces;
using NameApp.Domain.AccessService.Entities;
using NameApp.Domain.User.Entities;
using NameApp.Infrastructure.Data.Configuration;

namespace NameApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<UserEntity> Users { get; } = null!;
        public DbSet<GroupEntity> Groups { get; } = null!; 

        public DbSet<PermissionEntity> Permissions {  get; } = null!; 

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // add here any amount of configurations 
            builder.ApplyConfiguration(new UserConfiguration());
        }

    }
}
