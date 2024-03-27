using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KarmaMarketplace.Domain.User.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
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
