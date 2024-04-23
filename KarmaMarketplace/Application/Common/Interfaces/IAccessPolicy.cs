using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.User.Enums;

namespace KarmaMarketplace.Application.Common.Interfaces
{
    public interface IAccessPolicy
    {
        public Task<bool> CanAccess(UserRoles role, Guid? userId = null);
        public Task FailIfNoAccess(UserRoles role, Guid? userId = null); 
        public Task<bool> CanAccessOrSelf(Guid byUserId, UserRoles role, Guid? userId = null);
        public Task FailIfNotSelfOrNoAccess(Guid byUserId, UserRoles role, Guid? userId = null);
        public Task<UserEntity> GetCurrentUser(); 
    }
}
