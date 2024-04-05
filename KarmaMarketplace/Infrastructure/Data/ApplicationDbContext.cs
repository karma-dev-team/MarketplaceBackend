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
        public DbSet<UserEntity> Users { get; set;} 
        public DbSet<CategoryEntity> Categories { get; set;}  
        public DbSet<ProductEntity> Products { get; set;} 
        public DbSet<ReviewEntity> Reviews { get; set;} 
        public DbSet<GameEntity> Games { get; set;}
        public DbSet<AutoAnswerEntity> AutoAnswers { get; set; }
        public DbSet<TransactionProviderEntity> TransactionProviders { get; set; }
        public DbSet<PaymentSystemEntity> PaymentSystems { get; set; }
        public DbSet<OptionEntity> Options { get; set;} 
        public DbSet<PurchaseEntity> Purchases { get; set;} 
        public DbSet<ChatEntity> Chats { get; set;} 
        public DbSet<MessageEntity> Messages { get; set;} 
        public DbSet<ProductViewEntity> ProductViews { get; set;} 
        public DbSet<ChatReadRecord> ChatReads { get; set;} 
        public DbSet<TransactionEntity> Transactions { get; set;} 
        public DbSet<WalletEntity> Wallets { get; set;} 
        public DbSet<ImageEntity> Images { get; set;}  

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
