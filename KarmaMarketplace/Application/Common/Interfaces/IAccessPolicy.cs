using KarmaMarketplace.Domain.User.Enums;

namespace KarmaMarketplace.Application.Common.Interfaces
{
    public interface IAccessPolicy
    {
        public Task<bool> CanAccess(Guid userId, UserRoles role);
        public Task FailIfNoAccess(Guid userId, UserRoles role); 
    }
}
