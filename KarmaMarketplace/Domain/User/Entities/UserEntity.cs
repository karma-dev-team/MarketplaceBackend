using KarmaMarketplace.Domain.User.Enums;
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
        public string? TelegramId { get; set; } = null!;
        public bool Blocked { get; set; } = false; 
    }
}
