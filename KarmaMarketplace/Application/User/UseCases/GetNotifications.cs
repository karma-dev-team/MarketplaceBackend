using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.User.UseCases
{
    public class GetNotifications : BaseUseCase<GetNotificationsDto, ICollection<NotificationEntity>>
    {
        private IApplicationDbContext _context;
        private IAccessPolicy _accessPolicy; 

        public GetNotifications(IApplicationDbContext dbContext, IAccessPolicy accessPolicy) {
            _context = dbContext; 
            _accessPolicy = accessPolicy;
        }

        public async Task<ICollection<NotificationEntity>> Execute(GetNotificationsDto dto)
        {
            var query = _context.Notifications.AsQueryable(); 
            
            if (dto.UserId != null)
            {
                await _accessPolicy.FailIfNotSelfOrNoAccess(
                    (Guid)dto.UserId, Domain.User.Enums.UserRoles.Moderator); 

                query = query.Where(x => x.ToUserId == dto.UserId);
            }
            if (dto.Role != null)
            {
                await _accessPolicy.FailIfNoAccess(Domain.User.Enums.UserRoles.Moderator); 

                query = query
                    .Join(
                        _context.Users,
                        notification => notification.ToUserId,
                        user => user.Id,
                        (notification, user) => new { Notification = notification, User = user }
                    )
                    .Where(x => x.User.Role == dto.Role)
                    .Select(x => x.Notification); 
            }

            //query = query.Paginate(dto.Start, dto.Ends); 

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
