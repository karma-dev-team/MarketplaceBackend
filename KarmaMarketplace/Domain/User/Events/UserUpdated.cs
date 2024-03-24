using Microsoft.OpenApi.Any;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.User.Events
{
    public class UserUpdated(
        UserEntity user,
        string fieldName 
    ) : BaseEvent
    {
        public string FieldName { get; set; } = fieldName; 
        public UserEntity User { get; } = user;
    }
}
