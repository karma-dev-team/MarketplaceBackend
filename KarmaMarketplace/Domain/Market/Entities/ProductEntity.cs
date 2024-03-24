using KarmaMarketplace.Application.Market.Dto;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.Market.Enums;
using KarmaMarketplace.Domain.Market.ValueObjects;
using KarmaMarketplace.Domain.Market.Exceptions;
using KarmaMarketplace.Domain.Market.Events;

namespace KarmaMarketplace.Domain.Market.Entities
{
    public class ProductEntity : BaseAuditableEntity
    {
        // Часть смыслового ядра, нужны тесты!! 
        public UserEntity CreatedBy { get; set; } = null!;
        public CategoryEntity Category { get; set; } = null!;

        [Required, MaxLength(256)]
        public string Name { get; set; } = null!; 

        [Required, MaxLength(256)]
        public string Slug { get; set; } = null!;

        // Assuming Money is a decimal. If Money is a complex type, adjust accordingly.
        public Money? DiscountPrice { get; set; }

        public Money BasePrice { get; set; }

        public string Description { get; set; } = null!; 

        public ProductStatus Status { get; set; } 

        public UserEntity? BuyerUser { get; set; }

        [Column(TypeName = "jsonb")]
        public string Attributes { get; set; } = null!; 

        public static ProductEntity Create(
            UserEntity byUser, 
            CategoryEntity category, 
            string name,
            Money price, 
            string description,
            Dictionary<string, string> attributes,             
            ProductStatus status = ProductStatus.Processing) 
        {
            ProductEntity newProduct = new ProductEntity()
            {
                CreatedBy = byUser,
                Category = category,
                Name = name,
                BasePrice = price, 
                Description = description,
                Attributes = "{}", 
                Status = status, 
            };

            VerifyAttributes(attributes, category);

            newProduct.AddDomainEvent(
                new ProductCreated(newProduct)); 

            return newProduct;
        }

        public static void VerifyAttributes(
            Dictionary<string, string> attributes, 
            CategoryEntity category)
        {
            List<string> fields = category.Options
                .Select(x => x.Field)
                .ToList(); 
            foreach (var attribute in attributes)
            {
                if (!fields.Contains(attribute.Key))
                {
                    throw new IncorrectAttributes(attribute.Key, attribute.Value);
                }
            }
        }
    }
}
