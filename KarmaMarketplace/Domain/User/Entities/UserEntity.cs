using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.User.Enums;
using KarmaMarketplace.Domain.User.Events;
using KarmaMarketplace.Domain.User.Services;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.User.Entities
{
    public class UserEntity : BaseAuditableEntity
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string HashedPassword { get; set;} = null!;
        [Required] 
        [EmailAddress(ErrorMessage = "Email address is not correct")]
        public string Email { get; set; } = null!;
        public UserRoles Role { get; set; } = UserRoles.User; 
        public string? TelegramId {
            get => TelegramId;
            set
            {
                if (Blocked)
                {
                    throw new AccessDenied("blocked"); 
                }
                TelegramId = value;

                AddDomainEvent(
                    new UserUpdated(
                        user: this,
                        fieldName: "TelegramId"
                    )
                ); 
            } 
        }
        public bool Blocked { get; set; } = false;

        public static UserEntity Create(
            string UserName,
            string email,
            string password, 
            PasswordService passwordService)
        {
            var user = new UserEntity()
            {
                Email = email,
                UserName = UserName,
            };
            var hashedPassword = passwordService.HashPassword(user, password);

            user.HashedPassword = hashedPassword;

            user.AddDomainEvent(new UserCreated(User: user));

            return user;
        }

        public void Block()
        {
            Blocked = true; 
        }

        public void UpdatePassword(string oldPassword, string newPassword, PasswordService passwordService)
        {
            var result = passwordService.VerifyHashedPassword(
                this, HashedPassword, oldPassword);
            if (result != PasswordVerificationResult.Success)
            {
                throw new AccessDenied("Wrong password");
            }

            HashedPassword = passwordService.HashPassword(this, newPassword);
        }

        public void UpdateRole(
            UserRoles role)
        {
            Role = role;  

            AddDomainEvent(
                new UserUpdated(
                    user: this,
                    fieldName: "Role"
                )
             );
        }
    }
}
