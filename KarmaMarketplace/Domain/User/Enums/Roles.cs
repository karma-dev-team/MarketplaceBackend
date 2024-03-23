using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.User.Enums
{
    public enum UserRoles 
    {
        [Display(Name = "user")]
        User, 
        [Display(Name = "seller")]
        Seller,
        [Display(Name = "moderator")]
        Moderator,
        [Display(Name = "admin")]
        Admin,
        [Display(Name = "superadmin")]
        SuperAdmin,
        [Display(Name = "other")]
        Other,
    }
}
