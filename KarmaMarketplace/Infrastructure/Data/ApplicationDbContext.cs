using Microsoft.EntityFrameworkCore;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.User.Entities;
using System.Reflection;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Domain.Staff.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

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
        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<PurchaseEntity> Purchases { get; set; } 
        public DbSet<ChatEntity> Chats { get; set; } 
        public DbSet<MessageEntity> Messages { get; set; } 
        public DbSet<ProductViewEntity> ProductViews { get; set; } 
        public DbSet<ChatReadRecord> ChatReads { get; set; } 
        public DbSet<TransactionEntity> Transactions { get; set; } 
        public DbSet<WalletEntity> Wallets { get; set; } 
        public DbSet<FileEntity> Files { get; set; }  
        public DbSet<TicketEntity> Tickets { get; set; }
        public DbSet<TicketCommentEntity> TicketComments { get; set; }
        public DbSet<WarningEntity> UserWarnings { get; set; }

        private readonly IEventDispatcher _dispatcher; 

        public ApplicationDbContext(DbContextOptions options, IEventDispatcher eventDispatcher) : base(options) {
            _dispatcher = eventDispatcher;
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;  // ??????? TODO: need to research 
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await DispatchDomainEventsAsync();
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly()); 
        }

        private async Task DispatchDomainEventsAsync()
        {
            var entities = ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities.ToList().ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                try
                {
                    await _dispatcher.Dispatch(domainEvent, this);
                }
                catch (Exception ex)
                {
                    // Обработка ошибок при диспетчеризации событий
                    // Можно добавить логирование или другие механизмы обработки ошибок
                    throw; // Можно выбрасывать исключение или обрабатывать ошибку по желанию
                }
            }
        }
    }
}
