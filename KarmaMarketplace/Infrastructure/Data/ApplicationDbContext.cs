using Microsoft.EntityFrameworkCore;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Infrastructure.Data.Configuration;
using System.Reflection;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Files.Entities;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.Messging.Entities;

namespace KarmaMarketplace.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<UserEntity> Users { get; } = null!;
        public DbSet<CategoryEntity> Categories { get; } = null!; 
        public DbSet<ProductEntity> Products { get; } = null!;
        public DbSet<ReviewEntity> Reviews { get; } = null!;
        public DbSet<GameEntity> Games { get; } = null!;
        public DbSet<OptionEntity> Options { get; } = null!;
        public DbSet<PurchaseEntity> Purchases { get; } = null!;
        public DbSet<ChatEntity> Chats { get; } = null!;
        public DbSet<MessageEntity> Messages { get; } = null!;
        public DbSet<ProductViewEntity> ProductViews { get; } = null!;
        public DbSet<ChatReadRecord> ChatReads { get; } = null!;
        public DbSet<TransactionEntity> Transactions { get; } = null!;
        public DbSet<WalletEntity> Wallets { get; } = null!;

        public DbSet<ImageEntity> Images { get; } = null!; 

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
