using KarmaMarketplace.Domain.User.Enums;

namespace KarmaMarketplace.Application.User.Dto
{
    public class GetNotificationsDto
    {
        public Guid? UserId { get; set; }
        public UserRoles? Role { get; set; }
    }
}
