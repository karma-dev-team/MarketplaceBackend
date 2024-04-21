using KarmaMarketplace.Domain.Common;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.User.Enums;
using KarmaMarketplace.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Tests.Domain.UnitTests.Entities
{
    [TestFixture]
    public class UserEntityTests
    {
        [Test]
        public void CreateUser_WithValidParameters_ReturnsUserEntity()
        {
            // Arrange
            var userName = "testUser";
            var email = "test@example.com";
            var password = "password";

            var passwordHasher = new PasswordService(new PasswordHasher<UserEntity>()); 

            var user = UserEntity.Create(userName, email, password, passwordHasher);

            // Assert
            Assert.That(user, Is.Not.Null);
            Assert.That(user.UserName, Is.EqualTo(userName));
            Assert.That(user.Email, Is.EqualTo(email));
            Assert.That(user.Role, Is.EqualTo(UserRoles.SuperAdmin)); // Default role
        }

        [Test]
        public void Block_User_IsBlocked()
        {
            // Arrange
            var user = new UserEntity();

            // Act
            user.Block();

            // Assert
            Assert.That(user.Blocked, Is.True);
        }

        [Test]
        public void UpdatePassword_WithCorrectOldPassword_UpdatesPassword()
        {
            // Arrange
            var user = new UserEntity();
            var oldPassword = "oldPassword";
            var newPassword = "newPassword";
            var passwordService = new PasswordService(new PasswordHasher<UserEntity>()); // Assuming PasswordService is mocked or implemented
            user.HashedPassword = passwordService.HashPassword(user, oldPassword);

            // Act
            user.UpdatePassword(oldPassword, newPassword, passwordService);

            // Assert
            Assert.That(passwordService.VerifyHashedPassword(user, user.HashedPassword, newPassword), Is.EqualTo(PasswordVerificationResult.Success));
        }

        [Test]
        public void UpdatePassword_WithIncorrectOldPassword_ThrowsException()
        {
            // Arrange
            var user = new UserEntity();
            var oldPassword = "oldPassword";
            var newPassword = "newPassword";
            var incorrectOldPassword = "incorrectOldPassword";
            var passwordService = new PasswordService(new PasswordHasher<UserEntity>()); // Assuming PasswordService is mocked or implemented
            user.HashedPassword = passwordService.HashPassword(user, oldPassword);

            // Act & Assert
            Assert.Throws<AccessDenied>(() => user.UpdatePassword(incorrectOldPassword, newPassword, passwordService));
        }

        [Test]
        public void UpdateRole_WithValidRole_UpdatesRole()
        {
            // Arrange
            var user = new UserEntity();
            var newRole = UserRoles.Admin;

            // Act
            user.UpdateRole(newRole);

            // Assert
            Assert.That(user.Role, Is.EqualTo(newRole));
        }
    }
}
