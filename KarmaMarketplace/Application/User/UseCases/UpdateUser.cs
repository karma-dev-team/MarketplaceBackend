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
        private IUser User; 

        public UpdateUser( 
            IApplicationDbContext context,
            IAccessPolicy accessPolicy, 
            PasswordService passwordService, 
            IUser user)
        {
            User = user; 
            PasswordService = passwordService; 
            Context = context;
            AccessPolicy = accessPolicy;
        }

        public async Task<UserEntity> Execute(UpdateUserDto dto)
        {
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);

            Guard.Against.Null(user, message: "User does not exists");

            var byUser = await Context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            Guard.Against.Null(byUser, message: "User does not exists"); 
            
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
                if (byUser.Role == UserRoles.Owner)
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
