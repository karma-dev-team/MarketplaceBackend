using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Presentation.Web.Schemas
{
    public class LoginResponseSchema
    {
        [Required]
        public string AccessToken { get; set; } = null!; 
        [Required]
        public string ExpiresIn { get; set; } = null!; 
    }

    public class LoginUserSchema
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
