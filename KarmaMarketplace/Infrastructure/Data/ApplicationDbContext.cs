using Microsoft.EntityFrameworkCore;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Infrastructure.Data.Configuration;
using System.Reflection;

namespace KarmaMarketplace.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<UserEntity> Users { get; } = null!;

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly()); 
        }

    }
}
