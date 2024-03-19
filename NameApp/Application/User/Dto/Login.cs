using System.ComponentModel.DataAnnotations;

namespace NameApp.Application.User.Dto
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
