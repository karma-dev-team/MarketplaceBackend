using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.Payment.Enums;
using KarmaMarketplace.Domain.Payment.ValueObjects;
using KarmaMarketplace.Domain.User.Entities;
namespace Tests.Domain.UnitTests.Entities
{

    [TestFixture]
    public class TransactionEntityTests
    {
        [Test]
        public void TransactionEntity_Create_Should_Set_Default_Values_Correctly()
        {
            // Arrange
            var user = new UserEntity();
            var provider = new TransactionProviderEntity();

            // Act
            var transaction = TransactionEntity.Create(
                new Money(100, CurrencyEnum.Dollar),
                new Money(5, CurrencyEnum.Dollar),
                TransactionOperations.Sell,
                TransactionDirection.In,
                provider,
                user);

            // Assert
            Assert.That(transaction, Is.Not.Null);
            Assert.That(transaction.Status, Is.EqualTo(TransactionStatusEnum.Pending));
            Assert.That(transaction.CreatedByUser, Is.EqualTo(user));
        }

        [Test]
        public void TransactionEntity_Confirm_Should_Set_Status_And_CompletedAt_Correctly()
        {
            // Arrange
            var transaction = new TransactionEntity();

            // Act
            transaction.Confirm();

            // Assert
            Assert.That(transaction.Status, Is.EqualTo(TransactionStatusEnum.Confirmed));
            Assert.That(transaction.CompletedAt, Is.Not.Null);
        }

        [Test]
        public void TransactionEntity_Complete_Should_Set_Status_And_CompletedAt_Correctly()
        {
            // Arrange
            var transaction = new TransactionEntity();
            var status = TransactionStatusEnum.Failed;

            // Act
            transaction.Complete(status);

            // Assert
            Assert.That(transaction.Status, Is.EqualTo(status));
            Assert.That(transaction.CompletedAt, Is.Not.Null);
        }

        // Add more tests as needed
    }
}
