using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.Messaging.Enums;
using KarmaMarketplace.Domain.Messging.Enums;
using KarmaMarketplace.Domain.Messging.Events;
using KarmaMarketplace.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KarmaMarketplace.Domain.Messging.Entities
{
    public class ChatEntity : BaseAuditableEntity
    {
        [Required]
        public string Name { get; set; } = null!;

        public ICollection<ChatParticipant> Participants { get; set; } = [];
        public FileEntity? Image { get; set; } = null!;
        [Required]
        public bool Deleted { get; set; } = false; 
        [Required]
        public bool IsVerified { get; set; } = false;

        public IList<ChatReadRecord> ReadRecords { get; set; } = []; 

        [Required]
        public ChatTypes Type { get; set; } = ChatTypes.Private;
        public ICollection<MessageEntity> Messages { get; set; } = [];

        [NotMapped]
        public MessageEntity? LastMessage => 
            Messages?.OrderByDescending(m => m.CreatedAt).FirstOrDefault();

        [NotMapped]
        public int UnreadMessages
        {
            get
            {
                // Used in ChatDTO as not explicit property
                // TODO: somehow optimize it
                int unreadCount = 0;
                if (ReadRecords.Count == 0)
                {
                    return Messages.Count;
                }
                foreach (var message in Messages)
                {
                    if (message.CreatedAt > ReadRecords[ReadRecords.Count - 1].ReadAt)
                    {
                        unreadCount++;
                    }
                    else
                    {
                        break;
                    }
                }
                return unreadCount;
            }
        }

        public void AddParticipant(UserEntity user)
        {
            Participants.Add(user); 
        }

        public MessageEntity? PurchaseMessage(Guid purchaseId)
        {
            foreach (var message in Messages)
            {
                if (message.Purchase != null)
                {
                    if (message.Type == MessageTypes.Purchase && message.Purchase.Id == purchaseId)
                    {
                        return message; 
                    }
                }
            }
            return null; 
        }

        public void ReadMessages(ChatReadRecord chatRead)
        {
            ReadRecords.Add(chatRead);

            AddDomainEvent(new ChatRead(this)); 
        }

        public static ChatEntity CreateSupport(UserEntity user)
        {
            var chat = new ChatEntity();

            chat.Name = "Поддержка";
            chat.Participants = [user]; 
            chat.Type = ChatTypes.Support;
            //chat.Owner = user;
            chat.IsVerified = true;

            chat.AddDomainEvent(new ChatCreated(chat));

            return chat; 
        }

        public static ChatEntity CreatePrivate(List<UserEntity> userParticipants, string name, FileEntity? image)
        {
            var chat = new ChatEntity
            {
                Name = name,
                Participants = userParticipants,
                Image = image,
                Type = ChatTypes.Private,
                //Owner = owner
            };

            chat.AddDomainEvent(new ChatCreated(chat)); 

            return chat; 
        }
    }
}
