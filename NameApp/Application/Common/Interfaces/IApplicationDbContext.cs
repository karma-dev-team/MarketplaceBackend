using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NameApp.Domain.AccessService.Entities;
using NameApp.Domain.User.Entities;

namespace NameApp.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<UserEntity> Users { get; }
        DbSet<PermissionEntity> Permissions { get; }
        DbSet<GroupEntity> Groups { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class; 
    }
}
