using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.Market.Entities;

namespace KarmaMarketplace.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<UserEntity> Users { get; }

        DbSet<CategoryEntity> Categories { get; }

        DbSet<ProductEntity> Products { get; }

        DbSet<ReviewEntity> Reviews { get; }    

        DbSet<GameEntity> Games { get; }    

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class; 
    }
}
