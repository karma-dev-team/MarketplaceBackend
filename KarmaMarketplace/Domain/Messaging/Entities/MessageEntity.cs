using KarmaMarketplace.Domain.Messging.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.Messging.Events;
using KarmaMarketplace.Application.Payment.Dto;

namespace KarmaMarketplace.Domain.Messging.Entities
{
    public class MessageEntity : BaseAuditableEntity
    {
        [ForeignKey("ChatEntity")]
        [Required]
        public Guid ChatID { get; set; }

        [Required]
        public UserEntity FromUser { get; set; } = null!;

        [Required]
        public string Text { get; set; } = null!;

        [Required]
        public MessageTypes Type { get; set; } = MessageTypes.Text; 

        public FileEntity? Image { get; set; }         
        public ReviewEntity? Review { get; set; }
        [ForeignKey(nameof(ReviewEntity))]
        public Guid? ReviewId { get; set; } 

        public PurchaseEntity? Purchase { get; set; }

        public static MessageEntity CreateText(Guid chatId, UserEntity fromUser, string text)
        {
            var newMessage = new MessageEntity();

            newMessage.Text = text;
            newMessage.ChatID = chatId;
            newMessage.FromUser = fromUser;

            newMessage.AddDomainEvent(new MessageCreated(newMessage));

            return newMessage;
        }

        public static MessageEntity CreateWithPurchase(
            Guid chatId, 
            UserEntity fromUser, 
            PurchaseEntity purchase)
        {
            var newMessage = new MessageEntity();

            newMessage.ChatID = chatId; 
            newMessage.FromUser = fromUser;
            newMessage.Purchase = purchase;
            newMessage.Text = "PURCHASE";
            newMessage.Type = MessageTypes.Purchase;

            newMessage.AddDomainEvent(new MessageCreated(newMessage));

            return newMessage;
        }

        public static MessageEntity CreateWithReview(
            PurchaseEntity purchase, 
            Guid chatId , 
            UserEntity fromUser, 
            ReviewEntity review)
        {
            if (purchase == null) throw new ArgumentNullException(nameof(purchase));

            var newMessage = new MessageEntity()
            {
                Purchase = purchase,
                ChatID = chatId,
                FromUser = fromUser,
                //Review = review, 
                Type = MessageTypes.Review, 
                Text = "REVIEW"
            };

            newMessage.AddDomainEvent(new MessageCreated(newMessage)); 

            return newMessage; 
        }

        public static MessageEntity CreateWithImage(
            Guid chatId, 
            UserEntity fromUser, 
            FileEntity image)
        {
            var newMessage = new MessageEntity();

            newMessage.ChatID = chatId;
            newMessage.FromUser = fromUser;
            newMessage.Image = image;
            newMessage.Text = "IMAGE";
            newMessage.Type = MessageTypes.Image; 

            newMessage.AddDomainEvent(new MessageCreated(newMessage));

            return newMessage;
        }
    }
}
