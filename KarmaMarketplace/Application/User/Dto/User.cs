using KarmaMarketplace.Domain.User.Enums;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.User.Dto
{
    public class UserActionDto
    {
        [Required]
        public Guid ByUserId { get; set; }
    }

    public class UserActionOptionalDto
    {
        public Guid? ByUserId { get; set; }
    }

    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string EmailAddress { get; set; } = null!;
        public UserRoles Role { get; set; }
    }
    public class GetUserDto
    {
        public Guid? UserId { get; set; }
        public string? Email { get; set; } = null!;
    }

    public class GetListUserDto
    {
        public UserRoles? Role { get; set; }
        public string? Name { get; set; }
    }

    public class LoginDto
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }

    public class UpdateUserDto {
        [Required]
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public UserRoles? Role { get; set; }
        public string? Name { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? TelegramId { get; set; }
    }

    public class DeleteUserDto 
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
