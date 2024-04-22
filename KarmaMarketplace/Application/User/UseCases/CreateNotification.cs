using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.User.UseCases
{
    public class CreateNotification : BaseUseCase<CreateNotificationDto, NotificationCreatedDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy;


        public CreateNotification(IApplicationDbContext dbContext, IAccessPolicy accessPolicy) {
            _context = dbContext;
            _accessPolicy = accessPolicy;
        }

        public async Task<NotificationCreatedDto> Execute(CreateNotificationDto dto)
        {
            await _accessPolicy.FailIfNoAccess(Domain.User.Enums.UserRoles.Moderator);
            List<UserEntity> users = new List<UserEntity>(); 

            if (dto.Role != null)
            {
                await _accessPolicy.FailIfNoAccess(Domain.User.Enums.UserRoles.Owner);

                if (dto.Role == Domain.User.Enums.UserRoles.User)
                {
                    throw new AccessDenied("Impossible to ping that many users");
                }

                users = await _context.Users.AsNoTracking().Where(x => x.Role == dto.Role).ToListAsync();
            } else if (dto.UserId != null)
            {
                users = await _context.Users.AsNoTracking().Where(x => x.Id == dto.UserId).ToListAsync(); 
            }

            var countUsers = users.Count;

            foreach (var user in users)
            {
                var notification = NotificationEntity.CreateFromSystem(
                    dto.Title, dto.Text, user.Id, dto.Data); 

                _context.Notifications.Add(notification);
            }

            await _context.SaveChangesAsync();

            return new() { 
                UserIds = users.Select(x => x.Id).ToList(),  
                SentToUsers = countUsers,
            };
        }
    }
}
