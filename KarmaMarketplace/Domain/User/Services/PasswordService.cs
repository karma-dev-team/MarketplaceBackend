using Microsoft.AspNetCore.Identity;
using KarmaMarketplace.Domain.User.Entities;

namespace KarmaMarketplace.Domain.User.Services
{
    public class PasswordService
    {
        public PasswordHasher<UserEntity> PasswordHasher { get; set; }

        public PasswordService(PasswordHasher<UserEntity> passwordHasher) {
            PasswordHasher = passwordHasher; 
        }

        public string HashPassword(UserEntity user, string password)
        {
            return PasswordHasher.HashPassword(user, password);   
        }

        public PasswordVerificationResult VerifyHashedPassword(UserEntity user, string hashedPassword, string password)
        {
            return PasswordHasher.VerifyHashedPassword(user, hashedPassword, password);
        }
    }
}
