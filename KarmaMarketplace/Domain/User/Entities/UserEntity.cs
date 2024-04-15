using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.User.Enums;
using KarmaMarketplace.Domain.User.Events;
using KarmaMarketplace.Infrastructure;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(name: "TelegramId")]
        private string? _telegramId; // Закрытое поле для хранения значения
        public FileEntity? Image { get; set; }  

        [NotMapped]
        public string? TelegramId
        {
            get => _telegramId; // Возвращаем значение из закрытого поля
            set
            {
                if (Blocked)
                {
                    throw new AccessDenied("blocked");
                }
                _telegramId = value; // Устанавливаем значение в закрытое поле

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
            PasswordService passwordService, 
            UserRoles role = UserRoles.User)
        {
            var user = new UserEntity()
            {
                Email = email,
                UserName = UserName,
                Role = role, 
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

        public void UpdatePassword(string newPassword, PasswordService passwordService)
        {
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
