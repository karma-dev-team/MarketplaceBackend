using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Domain.UnitTests.Entities
{
    using KarmaMarketplace.Domain.Market.Entities;
    using KarmaMarketplace.Domain.Payment.Entities;
    using KarmaMarketplace.Domain.Payment.Enums;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class PurchaseEntityTests
    {
        [Test]
        public void PurchaseEntity_Create_Should_Set_Default_Values_Correctly()
        {
            // Arrange
            var wallet = new WalletEntity();
            var product = new ProductEntity();
            var transaction = new TransactionEntity
            {
                Amount = new KarmaMarketplace.Domain.Payment.ValueObjects.Money(
                    amount: 200, currency: CurrencyEnum.RussianRuble)
            };

            // Act
            var purchase = PurchaseEntity.Create(wallet, product, transaction);

            // Assert
            Assert.That(purchase, Is.Not.Null);
            Assert.That(purchase.Wallet, Is.EqualTo(wallet));
            Assert.That(purchase.Product, Is.EqualTo(product));
            Assert.That(purchase.Amount, Is.EqualTo(transaction.Amount));
            Assert.That(purchase.Currency, Is.EqualTo(transaction.Amount.Currency));
            Assert.That(purchase.Status, Is.EqualTo(PurchaseStatus.Processing));
        }

        [Test]
        public void PurchaseEntity_SetBoundChat_Should_Set_ChatId_Correctly()
        {
            // Arrange
            var purchase = new PurchaseEntity();
            var chatId = Guid.NewGuid();

            // Act
            purchase.SetBoundChat(chatId);

            // Assert
            Assert.That(purchase.ChatId, Is.EqualTo(chatId));
        }

        [Test]
        public void PurchaseEntity_Problem_Should_Set_Status_And_Description_Correctly()
        {
            // Arrange
            var purchase = new PurchaseEntity();
            var description = "Problem description";

            // Act
            purchase.Problem(description);

            // Assert
            Assert.That(purchase.Status, Is.EqualTo(PurchaseStatus.Rejected));
            Assert.That(purchase.StatusDescription, Is.EqualTo(description));
        }

        [Test]
        public void PurchaseEntity_Complete_Should_Set_Status_And_ReviewId_Correctly()
        {
            // Arrange
            var purchase = new PurchaseEntity();
            var review = new ReviewEntity { Id = Guid.NewGuid() };

            // Act
            purchase.Complete(review);

            // Assert
            Assert.That(purchase.Completed, Is.True);
            Assert.That(purchase.Status, Is.EqualTo(PurchaseStatus.Success));
            Assert.That(purchase.ReviewId, Is.EqualTo(review.Id));
        }

        // Add more tests as needed
    }

}
