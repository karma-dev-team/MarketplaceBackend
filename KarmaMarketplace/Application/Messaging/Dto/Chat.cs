using KarmaMarketplace.Application.Common.Models;

namespace KarmaMarketplace.Application.Messaging.Dto
{
    public class GetChatsListDto
    {
        public Guid? UserId { get; set; }
        public bool IsProblemChat { get; set; } = false;
    }

    public class GetChatDto
    {
        public Guid ChatId { get; set; }
    }

    public class GetChatMessagesDto : InputPagination
    {
        public Guid ChatId { get; set; }
    } 
}
