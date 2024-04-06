using KarmaMarketplace.Application.Common.Models;
using System.ComponentModel.DataAnnotations;

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

    public class InitiateProductChatDto
    {
        [Required]
        public Guid FromUserId { get; set; } 
        [Required] 
        public Guid ProductId { get; set; } 
        [Required]
        public Guid TransactionId { get; set; } 
    }
}
