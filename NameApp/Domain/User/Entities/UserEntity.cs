using NameApp.Domain.AccessService.Entities;
using NameApp.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace NameApp.Domain.User.Entities
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
        public PermissionEntity? Permission { get; set; } = null!; 
    }
}
