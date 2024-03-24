using KarmaMarketplace.Application.Common.Exceptions;
using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.User.Enums;
using KarmaMarketplace.Domain.User.Services;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.User.Interactors
{
    public class UpdateUser : BaseUseCase<UpdateUserDto, UserEntity>
    {
        private UserDomainService UserService; 
        private IApplicationDbContext Context;
        private IAccessPolicy AccessPolicy;

        public UpdateUser(UserDomainService userService, 
            IApplicationDbContext context,
            IAccessPolicy accessPolicy)
        {
            UserService = userService;
            Context = context;
            AccessPolicy = accessPolicy;
        }

        public async Task<UserEntity> Execute(UpdateUserDto dto)
        {
            var byUser = await Context.Users.FirstOrDefaultAsync(x => x.Id == dto.ByUserId);
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);
            if (user == null || byUser == null) 
                throw new EntityDoesNotExists(nameof(UserEntity), "");
            if (!await AccessPolicy.CanAccess(byUser.Id, Domain.User.Enums.UserRoles.Admin) || user.Id != byUser.Id)
            {
                throw new AccessDenied("be yourself"); 
            } 
            
            if (dto.Email != null)
            {
                user.Email = dto.Email;
            }
            if (dto.Name != null)
            {
                user.UserName = dto.Name;
            }
            if (dto.TelegramId != null)
            {
                user.TelegramId = dto.TelegramId; 
            }
            if (dto.NewPassword != null && dto.OldPassword != null) {
                UserService.UpdatePassword(
                    user, 
                    dto.OldPassword, 
                    dto.NewPassword); 
            }
            if (dto.Role != null) {
                UserService.UpdateRole(
                    user,
                    byUser,
                    (UserRoles)dto.Role); 
            }

            return user;
        }
    }
}
