using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Domain.Payment.Entities;

namespace KarmaMarketplace.Application.Common.Interfaces
{
     public interface IApplicationDbContext
     {
        DbSet<UserEntity> Users { get; set;}
        DbSet<TransactionProviderEntity> TransactionProviders { get; set; }
        DbSet<PaymentSystemEntity> PaymentSystems { get; set; }
        DbSet<CategoryEntity> Categories { get; set;} 
        DbSet<ProductEntity> Products { get; set;}
        DbSet<AutoAnswerEntity> AutoAnswers { get; set; }
        DbSet<ReviewEntity> Reviews { get; set;} 
        DbSet<GameEntity> Games { get; set;} 
        DbSet<OptionEntity> Options { get; set;} 
        DbSet<PurchaseEntity> Purchases { get; set;} 
        DbSet<ChatEntity> Chats { get; set;} 
        DbSet<MessageEntity> Messages { get; set;} 
        DbSet<ProductViewEntity> ProductViews { get; set;} 
        DbSet<ChatReadRecord> ChatReads { get; set;} 
        DbSet<TransactionEntity> Transactions { get; set;} 
        DbSet<WalletEntity> Wallets { get; set;} 
        DbSet<ImageEntity> Images { get; set;} 

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class; 
    }
}
