using KarmaMarketplace.Domain.Common;
using KarmaMarketplace.Domain.User.Events;
using Microsoft.AspNetCore.Identity;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.User.Enums;
using KarmaMarketplace.Domain.User.Events;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.User.Services
{
    public class UserEntityService
    {
        public IEventDispatcher EventDispatcher { get; set; }
        public PasswordService PasswordService { get; set; }

        public UserEntityService(IEventDispatcher eventDispatcher, PasswordService passwordService)
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

        public void Update(
            UserEntity byUser, 
            UserEntity user,
            Dictionary<string, object> value 
        )
        {
            foreach (var rawKey in value.Keys)
            {
                var key = rawKey.ToLower(); 
                if (key == "role")
                {
                    if (byUser.Role == UserRoles.SuperAdmin)
                    {
                        user.Role = (UserRoles)value[key];
                    }
                    else
                    {
                        throw new AccessDenied(null);
                    }
                }
                if (key == "newpassword")
                {
                    if (value.TryGetValue("OldPassword", out var oldpassword))
                    {
                        string oldPasswordAsString = (string)oldpassword; 

                        var result = PasswordService.VerifyHashedPassword(
                            user, user.HashedPassword, oldPasswordAsString); 
                        if (result != PasswordVerificationResult.Success)
                        {
                            throw new AccessDenied("Wrong password"); 
                        }

                        user.HashedPassword = PasswordService.HashPassword(user, (string)value[key]); 
                    } else
                    {
                        throw new Exception("old password is not present"); 
                    }
                }
            }

            EventDispatcher.Dispatch(
                new UserUpdated(
                    user: user, 
                    byUser: byUser, 
                    changedValues: value
                )
             ); 
        }
    }
}
