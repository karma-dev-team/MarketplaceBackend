using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.User.Exceptions;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure
{
    public class AccessPolicy : IAccessPolicy
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _currentUser;
        private readonly ILogger _logger;
        private UserEntity? _cachedCurrentUser; 

        public AccessPolicy(
            IApplicationDbContext context, IUser user, ILogger<AccessPolicy> logger) { 
            _context = context;
            _currentUser = user;
            _logger = logger;
            _cachedCurrentUser = null; 
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
            if (_cachedCurrentUser != null) {  
                if (_cachedCurrentUser.Id == userId || _cachedCurrentUser.Id == _currentUser.Id) {
                    return _cachedCurrentUser.Role >= role;
                }
            }
            if (userId == null)
            {
                if (_currentUser.Id == null)
                {
                    return false; 
                }
                return await CanAccess(role, _currentUser.Id);
            } 

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return false;
            }
            if (user.Blocked == true)
            {
                return false; 
            }

            _cachedCurrentUser = user; 
            return user.Role >= role; 
        }

        public async Task FailIfNoAccess(UserRoles role, Guid? userId = null)
        {
            var canAccess = await CanAccess(role, userId);

            if (!canAccess)
            {
                throw new AccessDenied($"Role: {role}, userId: {userId}, currentUserId: {_currentUser.Id}"); 
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
            if (!(await CanAccessOrSelf(role: role, byUserId: byUserId, userId: _currentUser.Id)))
            {
                throw new AccessDenied($"Role: {role}, userId: {userId}, currentUserId: {_currentUser.Id}, byUserId: {byUserId}");
            }
        }

        async public Task<UserEntity> GetCurrentUser()
        {
            if (_cachedCurrentUser?.Id != _currentUser.Id)
            {
                _cachedCurrentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUser.Id);
            }

            if (_cachedCurrentUser == null)
            {
                throw new AccessDenied("User is not authroized");
            }

            return _cachedCurrentUser; 
        }
    }
}
