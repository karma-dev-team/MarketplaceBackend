using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Market.Enums;
using KarmaMarketplace.Domain.Market.Exceptions;
using KarmaMarketplace.Domain.Payment.ValueObjects;
using KarmaMarketplace.Domain.User.Entities;

namespace Tests.Domain.UnitTests.Entities
{

    [TestFixture]
    public class ProductEntityTests
    {
        [Test]
        public void TestCreateMethod()
        {
            // Arrange
            var createdByUser = new UserEntity();
            var category = new CategoryEntity() {
                Options = [new OptionEntity() {
                    
                }]
            };
            var game = new GameEntity();
            var name = "Test Product";
            var price = new Money(10.0m); // Assuming Money is a class with a constructor taking a decimal
            var description = "Test Description";
            var attributes = new Dictionary<string, string>();
            var images = new List<FileEntity>();

            // Act
            var product = ProductEntity.Create(createdByUser, category, game, name, price, description, attributes, images);

            // Assert
            Assert.That(product != null);
            Assert.That(product.CreatedBy == createdByUser);
            Assert.That(product.Category == category);
            Assert.That(product.Name == name);
            Assert.That(product.BasePrice == price);
            Assert.That(product.Description == description);
            Assert.That(product.Game == game);
            Assert.That(product.Status == ProductStatus.Processing);
            Assert.That(!string.IsNullOrEmpty(product.Slug));
        }

        [Test]
        public void TestVerifyAttributesMethod()
        {
            // Arrange
            var category = new CategoryEntity
            {
                Options = new List<OptionEntity>
            {
                OptionEntity.CreateSelector("Color", "Red", "Appearance", "color", 1),
                OptionEntity.CreateSelector("Size", "Medium", "Appearance", "size", 2)
            }
            };
            var validAttributes = new Dictionary<string, string>
            {
                { "size", "Red" },
                { "color", "Medium" }
            };
            var invalidAttributes = new Dictionary<string, string>
            {
                { "Color", "Blue" }, // Invalid color
                { "Weight", "Heavy" } // Weight attribute not defined in options
            };

            // Act & Assert
            Assert.That(() => ProductEntity.VerifyAttributes(validAttributes, category), Throws.Nothing);
            Assert.That(() => ProductEntity.VerifyAttributes(invalidAttributes, category), Throws.TypeOf<IncorrectAttributes>());
        }

        [Test]
        public void TestSoldMethod()
        {
            var productId = Guid.NewGuid();

            // Arrange
            var product = new ProductEntity() { Id = productId };
            var review = new ReviewEntity { Product = new ProductEntity() }; // Different product ID intentionally

            // Act & Assert
            Assert.That(() => product.Sold(review), Throws.Exception.TypeOf<Exception>());
        }
    }
}
