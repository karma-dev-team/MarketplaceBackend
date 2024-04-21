using KarmaMarketplace.Domain.Messaging.Enums;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Domain.User.Entities;

namespace Tests.Domain.UnitTests.Entities
{
    [TestFixture]
    public class MessagingTests
    {
        [Test]
        public void CreateText_WithValidParameters_ReturnsMessageEntity()
        {
            // Arrange
            var chatId = Guid.NewGuid();
            var fromUser = new UserEntity();
            var text = "Test text";

            // Act
            var message = MessageEntity.CreateText(chatId, fromUser, text);

            // Assert
            Assert.That(message, Is.Not.Null);
            Assert.That(message.ChatID, Is.EqualTo(chatId));
            Assert.That(message.FromUser, Is.EqualTo(fromUser));
            Assert.That(message.Text, Is.EqualTo(text));
        }

        [Test]
        public void CreateText_WithTooLongText_ThrowsException()
        {
            // Arrange
            var chatId = Guid.NewGuid();
            var fromUser = new UserEntity();
            var longText = new string('a', 5000); // Long text

            // Act & Assert
            Assert.Throws<Exception>(() => MessageEntity.CreateText(chatId, fromUser, longText));
        }
    }

    [TestFixture]
    public class ChatReadRecordTests
    {
        [Test]
        public void Constructor_WithValidParameters_SetsProperties()
        {
            // Arrange
            var readById = Guid.NewGuid();
            var chatId = Guid.NewGuid();

            // Act
            var chatReadRecord = new ChatReadRecord(readById, chatId);

            // Assert
            Assert.That(chatReadRecord.ReadById, Is.EqualTo(readById));
            Assert.That(chatReadRecord.ChatId, Is.EqualTo(chatId));
            Assert.That((DateTime.UtcNow - chatReadRecord.ReadAt).TotalSeconds, Is.LessThan(1)); // Check if ReadAt is set to current time
        }

        // Additional tests for other functionalities if any
    }

    [TestFixture]
    public class ChatEntityTests
    {
        [Test]
        public void CreateSupport_WithValidUser_ReturnsChatEntity()
        {
            // Arrange
            var user = new UserEntity();

            // Act
            var chat = ChatEntity.CreateSupport(user);

            // Assert
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.Name, Is.EqualTo("Поддержка"));
            Assert.That(chat.Type, Is.EqualTo(ChatTypes.Support));
            Assert.That(chat.IsVerified, Is.True);
            Assert.That(chat.Participants, Contains.Item(user));
        }

        // Additional tests for other functionalities if any
    }
}
