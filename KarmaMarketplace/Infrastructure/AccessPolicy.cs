using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure
{
    public class AccessPolicy : IAccessPolicy
    {
        private IApplicationDbContext _context; 

        public AccessPolicy(IApplicationDbContext context) { 
            _context = context;
        }

        public async Task<bool> CanAccess(Guid userId, UserRoles role)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId); 
            if (user == null)
            {
                return false;
            }
            if (user.Blocked == true)
            {
                return false; 
            }
            return user.Role >= role; 
        }

        public async Task FailIfNoAccess(Guid userId, UserRoles role)
        {
            if (!(await CanAccess(userId, role)))
            {
                throw new AccessDenied(""); 
            }
        }
    }
}
