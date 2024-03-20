using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.User.Enums
{
    public enum UserRoles 
    {
        [Display(Name = "user")]
        User = 1, 
        [Display(Name = "seller")]
        Seller = 2,
        [Display(Name = "moderator")]
        Moderator = 3,
        [Display(Name = "admin")]
        Admin = 4,
        [Display(Name = "superadmin")]
        SuperAdmin = 5,
        [Display(Name = "other")]
        Other = 228,
    }
}
