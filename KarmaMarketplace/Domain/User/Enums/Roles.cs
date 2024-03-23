using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.User.Enums
{
    public enum UserRoles 
    {
        // DONT CHANGE THE ORDER
        [Display(Name = "other")]
        Other,
        [Display(Name = "user")]
        User, 
        [Display(Name = "seller")]
        Seller,
        [Display(Name = "moderator")]
        Moderator,
        [Display(Name = "admin")]
        Admin,
        [Display(Name = "owner")]
        Owner, 
        [Display(Name = "superadmin")]
        SuperAdmin,
    }
}
