using KarmaMarketplace.Domain.User.Events;
using Microsoft.AspNetCore.Identity;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.User.Enums;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using KarmaMarketplace.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KarmaMarketplace.Domain.User.Services
{
    public class UserDomainService
    {
        public IEventDispatcher EventDispatcher { get; set; }
        public PasswordService PasswordService { get; set; }

        public UserDomainService(IEventDispatcher eventDispatcher, PasswordService passwordService)
        {
            Guard.Against.Null(eventDispatcher, message: "Event dispatcher is not found.");

            EventDispatcher = eventDispatcher; 
            PasswordService = passwordService;
        }

        public UserEntity Create(
            string UserName, 
            string email, 
            string password
        )
        {
            var user = new UserEntity()
            {
                Email = email, 
                UserName = UserName,
            };
            var hashedPassword = PasswordService.HashPassword(user, password);

            user.HashedPassword = hashedPassword;

            EventDispatcher.Dispatch(new UserCreated(User: user));

            return user; 
        }

        public void UpdateRole(
            UserEntity user, 
            UserEntity byUser, 
            UserRoles role
            )
        {
            if (byUser.Role == UserRoles.SuperAdmin)
            {
                user.Role = role;
            }
            else
            {
                throw new AccessDenied(null);
            }

            EventDispatcher.Dispatch(
                new UserUpdated(
                    user: user,
                    byUser: user
                )
             );
        }

        public void UpdatePassword(
            UserEntity user, 
            string oldPassword, 
            string newPassword
        )
        {
            var result = PasswordService.VerifyHashedPassword(
                user, user.HashedPassword, oldPassword);
            if (result != PasswordVerificationResult.Success)
            {
                throw new AccessDenied("Wrong password");
            }

            user.HashedPassword = PasswordService.HashPassword(user, newPassword);

            EventDispatcher.Dispatch(
                new UserUpdated(
                    user: user, 
                    byUser: user 
                )
             ); 
        }
    }
}
