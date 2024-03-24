using Microsoft.EntityFrameworkCore;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Infrastructure.Data.Configuration;
using System.Reflection;
using KarmaMarketplace.Domain.Market.Entities;

namespace KarmaMarketplace.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<UserEntity> Users { get; } = null!;

        public DbSet<CategoryEntity> Categories { get; } = null!; 

        public DbSet<ProductEntity> Products { get; } = null!;

        public DbSet<ReviewEntity> Reviews { get; } = null!;

        public DbSet<GameEntity> Games { get; } = null!;

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
