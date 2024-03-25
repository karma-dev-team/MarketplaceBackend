using KarmaMarketplace.Application.Common.Exceptions;
using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.User.Enums;
using KarmaMarketplace.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KarmaMarketplace.Application.User.Interactors
{
    public class UpdateUser : BaseUseCase<UpdateUserDto, UserEntity>
    {
        private IApplicationDbContext Context;
        private IAccessPolicy AccessPolicy;
        private PasswordService PasswordService; 

        public UpdateUser( 
            IApplicationDbContext context,
            IAccessPolicy accessPolicy, 
            PasswordService passwordService)
        {
            PasswordService = passwordService; 
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
                user.UpdatePassword(
                    oldPassword: dto.OldPassword, 
                    newPassword: dto.NewPassword, 
                    passwordService: PasswordService); 
            }
            if (dto.Role != null) {
                if (byUser.Role == UserRoles.SuperAdmin)
                {
                    user.UpdateRole(
                        (UserRoles)dto.Role);
                }
                else
                {
                    throw new AccessDenied(null);
                }
            }

            return user;
        }
    }
}
