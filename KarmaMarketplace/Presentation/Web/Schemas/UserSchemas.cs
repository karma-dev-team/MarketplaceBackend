using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Domain.User.Entities;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Presentation.Web.Schemas
{
    public class UserScheme
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;

        public static UserScheme FromEntity(UserEntity entity)
        {
            return new UserScheme {
                Id = entity.Id,
                Name = entity.UserName, 
                Email = entity.Email 
            };
        }
    }

    public class CreateUserScheme {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
    }

    public class WarnUserScheme {
        public string Reason { get; set; } = null!;
    }
}
