using KarmaMarketplace.Application.Market.Dto;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.Market.Enums;
using KarmaMarketplace.Domain.Market.Exceptions;
using KarmaMarketplace.Domain.Market.Events;
using KarmaMarketplace.Domain.Files.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using KarmaMarketplace.Domain.Payment.ValueObjects;

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

        public Money BasePrice { get; set; } = new Money(0); 

        public string Description { get; set; } = null!; 

        public ProductStatus Status { get; set; } 

        public UserEntity? BuyerUser { get; set; }

        [Column(TypeName = "jsonb")]
        public string Attributes { get; set; } = null!;
        public ICollection<FileEntity> Images { get; set; } = [];
        public ICollection<ProductViewEntity> ProductViews { get; set; } = [];

        [NotMapped]
        public Money CurrentPrice { get
            {
                if (ReferenceEquals(DiscountPrice, null))
                {
                    return BasePrice; 
                } else
                {
                    return DiscountPrice;
                }
            }} 

        public static ProductEntity Create(
            UserEntity byUser, 
            CategoryEntity category, 
            string name,
            Money price, 
            string description,
            Dictionary<string, string> attributes,             
            ICollection<FileEntity> images,
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
                Images = images, 
                Slug = Guid.NewGuid().ToString(),
            };

            VerifyAttributes(attributes, category);

            // TODO: Possible security breach?
            newProduct.Attributes = JsonSerializer.Serialize(attributes);

            newProduct.AddDomainEvent(
                new ProductCreated(newProduct)); 

            return newProduct;
        }

        public void RegisterView(
            UserEntity byUser)
        {
            ProductViews.Add(new ProductViewEntity(userId: byUser.Id, productId: Id, info: "{}"));

            AddDomainEvent(new ProductViewed(this)); 
        }

        public int CountViews(
            DateTime startDate,
            DateTime endDate)
        {
            var filteredVisits = ProductViews
                .Where(visit => visit.CreatedAt >= startDate && visit.CreatedAt <= endDate)
                .ToList();

            return filteredVisits.Count; 
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

        public void Sold(ReviewEntity review)
        {
            if (review.Product.Id != Id)
            {
                throw new Exception("Critical, bug! Review product id is not bound to product"); 
            }
            Status = ProductStatus.Sold;
            AddDomainEvent(new ProductSold(this, DateTime.UtcNow)); 
        }
    }
}
