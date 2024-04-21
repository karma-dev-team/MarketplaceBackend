using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.User.Exceptions;
using KarmaMarketplace.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure
{
    public class AccessPolicy : IAccessPolicy
    {
        private IApplicationDbContext _context;
        private IUser currentUser;
        private ILogger _logger;    

        public AccessPolicy(
            IApplicationDbContext context, IUser user, ILogger<AccessPolicy> logger) { 
            _context = context;
            currentUser = user;
            _logger = logger;
        }

        public static void UnauthorizedIfNull(Guid? userId)
        {
            if (userId == null)
            {
                throw new Unauthorized(); 
            }
        }

        public async Task<bool> CanAccess(UserRoles role, Guid? userId = null)
        {
            if (userId == null)
            {
                if (currentUser.Id == null)
                {
                    return false; 
                }
                return await CanAccess(role, currentUser.Id);
            } 

            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userId);
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

        public async Task FailIfNoAccess(UserRoles role, Guid? userId = null)
        {
            var canAccess = await CanAccess(role, userId);

            if (!canAccess)
            {
                throw new AccessDenied($"Role: {role}, userId: {userId}, currentUserId: {currentUser.Id}"); 
            }
        }

        public async Task<bool> CanAccessOrSelf(Guid byUserId, UserRoles role, Guid? userId = null)
        {
            if (userId == byUserId)
            {
                return true; 
            }
            return userId == byUserId || await CanAccess(role, byUserId); 
        }

        public async Task FailIfNotSelfOrNoAccess(Guid byUserId, UserRoles role, Guid? userId = null)
        {
            if (!(await CanAccessOrSelf(role: role, byUserId: byUserId, userId: currentUser.Id)))
            {
                throw new AccessDenied($"Role: {role}, userId: {userId}, currentUserId: {currentUser.Id}, byUserId: {byUserId}");
            }
        }
    }
}
