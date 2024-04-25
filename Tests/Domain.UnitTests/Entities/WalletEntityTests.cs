using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.Payment.Enums;
using KarmaMarketplace.Domain.Payment.Exceptions;
using KarmaMarketplace.Domain.Payment.ValueObjects;
using KarmaMarketplace.Domain.User.Entities;
using System;

namespace Tests.Domain.UnitTests.Entities
{

    [TestFixture]
    public class WalletEntityTests
    {
        [Test]
        public void WalletEntity_Create_Should_Set_Default_Values_Correctly()
        {
            // Arrange
            var user = new UserEntity();

            // Act
            var wallet = WalletEntity.Create(user);

            // Assert
            Assert.That(wallet, Is.Not.Null);
            Assert.That(wallet.User, Is.EqualTo(user));
            Assert.That(wallet.Currency, Is.EqualTo(CurrencyEnum.RussianRuble));
            Assert.That(wallet.Frozen.Amount, Is.EqualTo(0));
            Assert.That(wallet.AvailableBalance.Amount, Is.EqualTo(1000));
            Assert.That(wallet.PendingIncome.Amount, Is.EqualTo(0));
            Assert.That(wallet.Blocked, Is.False);
        }

        [Test]
        public void WalletEntity_AddBalance_Should_Increase_AvailableBalance_Correctly()
        {
            // Arrange
            var wallet = new WalletEntity();
            var initialBalance = wallet.AvailableBalance.Amount;
            var amountToAdd = new Money(100, CurrencyEnum.RussianRuble);

            // Act
            wallet.AddBalance(amountToAdd);

            // Assert
            Assert.That(wallet.AvailableBalance.Amount, Is.EqualTo(initialBalance + amountToAdd.Amount));
        }

        [Test]
        public void WalletEntity_DecreaseBalance_Should_Decrease_AvailableBalance_Correctly()
        {
            // Arrange
            var wallet = new WalletEntity();
            wallet.AddBalance(new Money(500, CurrencyEnum.RussianRuble));
            var initialBalance = wallet.AvailableBalance.Amount;
            var amountToDecrease = new Money(200, CurrencyEnum.RussianRuble);

            // Act
            wallet.DecreaseBalance(amountToDecrease);

            // Assert
            Assert.That(wallet.AvailableBalance.Amount, Is.EqualTo(initialBalance - amountToDecrease.Amount));
        }

        [Test]
        public void WalletEntity_DecreaseBalance_Should_Throw_Exception_When_Balance_Is_Not_Enough()
        {
            // Arrange
            var wallet = new WalletEntity()
            {
                AvailableBalance = new Money(100, CurrencyEnum.RussianRuble)
            };
            var amountToDecrease = new Money(1000, CurrencyEnum.RussianRuble); // Greater than available balance

            // Act & Assert
            Assert.Throws<NotEnoughMoneyException>(() => wallet.DecreaseBalance(amountToDecrease));
        }

        [Test]
        public void WalletEntity_IncreaseBalance_Should_Increase_AvailableBalance_Correctly()
        {
            // Arrange
            var wallet = new WalletEntity();
            var initialBalance = wallet.AvailableBalance.Amount;
            var amountToIncrease = new Money(200, CurrencyEnum.RussianRuble);

            // Act
            wallet.IncreaseBalance(amountToIncrease);

            // Assert
            Assert.That(wallet.AvailableBalance.Amount, Is.EqualTo(initialBalance + amountToIncrease.Amount));
        }

        [Test]
        public void WalletEntity_RollbackTransaction_Should_Increase_AvailableBalance_When_TransactionDirection_Is_In()
        {
            // Arrange
            var wallet = new WalletEntity();
            var createdBy = new UserEntity(); 
            var initialBalance = wallet.AvailableBalance.Amount;
            var transaction = new TransactionEntity { Amount = new Money(300, CurrencyEnum.RussianRuble), Direction = TransactionDirection.In, CreatedByUser = createdBy };

            // Act
            wallet.RollbackTransaction(transaction);

            // Assert
            Assert.That(wallet.AvailableBalance.Amount, Is.EqualTo(initialBalance + transaction.Amount.Amount));
        }

        [Test]
        public void WalletEntity_ConfirmTransaction_Should_Decrease_AvailableBalance_When_TransactionDirection_Is_Out()
        {
            // Arrange
            var wallet = new WalletEntity();
            var createdBy = new UserEntity();
            wallet.AddBalance(new Money(500, CurrencyEnum.RussianRuble));
            var initialBalance = wallet.AvailableBalance.Amount;
            var transaction = new TransactionEntity { Amount = new Money(200, CurrencyEnum.RussianRuble), Direction = TransactionDirection.Out, CreatedByUser = createdBy };
            var otherWallet = new WalletEntity() { AvailableBalance = new Money(400, CurrencyEnum.RussianRuble) };

            // Act
            wallet.ConfirmTransaction(transaction, otherWallet);

            // Assert
            Assert.That(wallet.AvailableBalance.Amount, Is.EqualTo(initialBalance - transaction.Amount.Amount));
        }

        [Test]
        public void WalletEntity_FreezeAmount_Should_Increase_Frozen_Balance()
        {
            // Arrange
            var wallet = new WalletEntity();
            var initialFrozenAmount = wallet.Frozen.Amount;
            var amountToFreeze = new Money(100, CurrencyEnum.RussianRuble);

            // Act
            wallet.FreezeAmount(amountToFreeze);

            // Assert
            Assert.That(wallet.Frozen.Amount, Is.EqualTo(initialFrozenAmount + amountToFreeze.Amount));
        }

        [Test]
        public void WalletEntity_UnfreezeAmount_Should_Decrease_Frozen_Balance()
        {
            // Arrange
            var wallet = new WalletEntity();
            wallet.FreezeAmount(new Money(200, CurrencyEnum.RussianRuble));
            var initialFrozenAmount = wallet.Frozen.Amount;
            var amountToUnfreeze = new Money(100, CurrencyEnum.RussianRuble);

            // Act
            wallet.UnfreezeAmount(amountToUnfreeze);

            // Assert
            Assert.That(wallet.Frozen.Amount, Is.EqualTo(initialFrozenAmount - amountToUnfreeze.Amount));
        }

        [Test]
        public void WalletEntity_UnfreezeAmount_Should_Throw_Exception_When_Unfreezing_More_Than_Frozen_Balance()
        {
            // Arrange
            var wallet = new WalletEntity();
            var amountToUnfreeze = new Money(100, CurrencyEnum.RussianRuble); // Greater than frozen balance

            // Act & Assert
            Assert.Throws<ArgumentException>(() => wallet.UnfreezeAmount(amountToUnfreeze));
        }

        [Test]
        public void WalletEntity_Block_Should_Set_Blocked_Flag()
        {
            // Arrange
            var wallet = new WalletEntity();
            var reason = "Blocked for testing";

            // Act
            wallet.Block(reason);

            // Assert
            Assert.That(wallet.Blocked, Is.True);
        }
    }
}
