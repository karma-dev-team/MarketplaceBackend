using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.Messaging.Entities;
using KarmaMarketplace.Domain.Messaging.Enums;
using KarmaMarketplace.Domain.Messging.Enums;
using KarmaMarketplace.Domain.Messging.Events;
using KarmaMarketplace.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace KarmaMarketplace.Domain.Messging.Entities
{
    public class ChatEntity : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;

        // Handling the Participants as a many-to-many relationship might require an additional entity or configuration outside this code snippet
        //public UserEntity Owner { get; set; } = null!;

        public ICollection<UserEntity> Participants { get; set; } = [];
        public ImageEntity? Image { get; set; } = null!;
        public bool Deleted { get; set; } = false; 
        public bool IsVerified { get; set; } = false;

        public IList<ChatReadRecord> ReadRecords { get; set; } = []; 

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
            //chat.Participants = [new ChatParticipant() { ChatId = chat.Id, UserId = user.Id}]; 
            //chat.Owner = user;
            chat.IsVerified = true;

            chat.AddDomainEvent(new ChatCreated(chat));

            return chat; 
        }

        public static ChatEntity CreatePrivate(List<UserEntity> userParticipants, string name, UserEntity owner, ImageEntity? image)
        {
            if (!userParticipants.Contains(owner))
                throw new ArgumentException("Owner not in participants.");



            var chat = new ChatEntity
            {
                Name = name,
                Image = image,
                Type = ChatTypes.Private,
                Participants = userParticipants, 
                //Owner = owner
            };

            //ICollection<ChatParticipant> participants = [];

            //foreach (var userPart in userParticipants)
            //{
            //    participants.Add(new ChatParticipant() { ChatId = chat.Id, UserId = userPart.Id });
            //}

            //chat.Participants = participants;

            chat.AddDomainEvent(new ChatCreated(chat)); 

            return chat; 
        }
    }
}
