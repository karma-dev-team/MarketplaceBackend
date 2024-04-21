using KarmaMarketplace.Domain.Payment.Enums;
using KarmaMarketplace.Domain.Payment.ValueObjects;
using NUnit.Framework;

namespace KarmaMarketplaceTests.Domain.UnitTests.ValueObjects
{
    public class MoneyTests
    {
        [Test]
        public void Constructor_SetsAmountAndCurrency()
        {
            // Arrange
            decimal amount = 100.50m;
            CurrencyEnum currency = CurrencyEnum.Dollar;
             
            // Act
            Money money = new Money(amount, currency);

            // Assert
            Assert.Equals(amount, money.Amount);
            Assert.Equals(currency, money.Currency);
        }

        [Test]
        public void Constructor_DefaultCurrencyIsRussianRuble()
        {
            // Arrange
            decimal amount = 100.50m;

            // Act
            Money money = new Money(amount);

            // Assert
            Assert.Equals(amount, money.Amount);
            Assert.Equals(CurrencyEnum.RussianRuble, money.Currency);
        }

        [Theory]
        [TestCase(100.50, CurrencyEnum.Dollar, 100.50, CurrencyEnum.Dollar, true)]
        [TestCase(100.50, CurrencyEnum.Dollar, 100.50, CurrencyEnum.Euro, false)]
        public void Equals_ReturnsExpectedResult(decimal amount1, CurrencyEnum currency1, decimal amount2, CurrencyEnum currency2, bool expectedResult)
        {
            // Arrange
            Money money1 = new Money(amount1, currency1);
            Money money2 = new Money(amount2, currency2);

            // Act
            bool result = money1.Equals(money2);

            // Assert
            Assert.Equals(expectedResult, result);
        }

        [Theory]
        [TestCase(100.50, CurrencyEnum.Dollar, 100.50, CurrencyEnum.Dollar, true)]
        [TestCase(100.50, CurrencyEnum.Dollar, 200.50, CurrencyEnum.Dollar, false)]
        public void EqualityOperators_ReturnsExpectedResult(decimal amount1, CurrencyEnum currency1, decimal amount2, CurrencyEnum currency2, bool expectedResult)
        {
            // Arrange
            Money money1 = new Money(amount1, currency1);
            Money money2 = new Money(amount2, currency2);

            // Act & Assert
            Assert.Equals(expectedResult, money1 == money2);
            Assert.Equals(!expectedResult, money1 != money2);
        }

        [Test]
        public void GreaterThanAndLessThanOperators_ThrowArgumentNullException_WhenEitherOperandIsNull()
        {
            // Arrange
            Money money1 = new Money(100.50m, CurrencyEnum.Dollar);
            Money money2 = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => { var result = money1 > money2; });
            Assert.Throws<ArgumentNullException>(() => { var result = money1 < money2; });
        }

        [Test]
        public void GreaterThanAndLessThanOperators_ThrowArgumentException_WhenCurrenciesAreNotEqual()
        {
            // Arrange
            Money money1 = new Money(100.50m, CurrencyEnum.Dollar);
            Money money2 = new Money(200.50m, CurrencyEnum.Euro);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => { var result = money1 > money2; });
            Assert.Throws<ArgumentException>(() => { var result = money1 < money2; });
        }

        [Theory]
        [TestCase(100.50, 200.50, CurrencyEnum.Dollar)]
        [TestCase(200.50, 100.50, CurrencyEnum.Dollar)]
        public void PlusAndMinusOperators_ReturnExpectedResult(decimal amount1, decimal amount2, CurrencyEnum currency)
        {
            // Arrange
            Money money1 = new Money(amount1, currency);
            Money money2 = new Money(amount2, currency);

            // Act
            Money sum = money1 + money2;
            Money difference = money1 - money2;

            // Assert
            Assert.Equals(amount1 + amount2, sum.Amount);
            Assert.Equals(amount1 - amount2, difference.Amount);
            Assert.Equals(currency, sum.Currency);
            Assert.Equals(currency, difference.Currency);
        }
    }
}
