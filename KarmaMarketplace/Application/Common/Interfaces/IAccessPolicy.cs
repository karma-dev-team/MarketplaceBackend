using KarmaMarketplace.Domain.User.Enums;

namespace KarmaMarketplace.Application.Common.Interfaces
{
    public interface IAccessPolicy
    {
        public Task<bool> CanAccess(Guid userId, UserRoles role);
        public Task FailIfNoAccess(Guid userId, UserRoles role); 
        public Task CanAccessOrSelf(Guid userId, Guid byUserId, UserRoles role);
        public Task FailIfNotSelfOrNoAccess(Guid userId, Guid isSelfId, UserRoles role); 
    }
}
