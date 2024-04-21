using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Market.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Domain.UnitTests.Entities
{
    [TestFixture]
    public class OptionEntityTests
    {
        [Test]
        public void TestCreateRangeMethod()
        {
            // Arrange
            var label = "Test Range";
            var value = "Test Value";
            var field = "Test Field";
            var sequence = 1;
            var min = 0;
            var max = 100;

            // Act
            var option = OptionEntity.CreateRange(label, value, field, sequence, min, max);

            // Assert
            Assert.That(option != null);
            Assert.That(option.Label == label);
            Assert.That(option.Value == value);
            Assert.That(option.Field == field);
            Assert.That(option.Sequence == sequence);
            Assert.That(option.Type == OptionTypes.Range);
            Assert.That(option.ValueRangeMin == min);
            Assert.That(option.ValueRangeMax == max);
        }

        [Test]
        public void TestCreateSwitchMethod()
        {
            // Arrange
            var label = "Test Switch";
            var field = "Test Field";
            var sequence = 2;

            // Act
            var option = OptionEntity.CreateSwitch(label, field, sequence);

            // Assert
            Assert.That(option != null);
            Assert.That(option.Label == label);
            Assert.That(option.Field == field);
            Assert.That(option.Sequence == sequence);
            Assert.That(option.Type == OptionTypes.Switch);
        }

        [Test]
        public void TestCreateSelectorMethod()
        {
            // Arrange
            var label = "Test Selector";
            var value = "Test Value";
            var group = "Test Group";
            var field = "Test Field";
            var sequence = 3;

            // Act
            var option = OptionEntity.CreateSelector(label, value, group, field, sequence);

            // Assert
            Assert.That(option != null);
            Assert.That(option.Label == label);
            Assert.That(option.Value == value);
            Assert.That(option.Group == group);
            Assert.That(option.Field == field);
            Assert.That(option.Sequence == sequence);
            Assert.That(option.Type == OptionTypes.Selector);
        }
    }
}
