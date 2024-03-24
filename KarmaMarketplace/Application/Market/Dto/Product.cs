using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.Market.Dto
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public Guid GameId { get; set; }
        [Required]
        public decimal Price { get; set; }
        public Dictionary<string, string> Attributes { get; set; } = new();
        public ICollection<Guid> Images { get; set; } = []; 
    }

    public class UpdateProductDto
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Guid ByUserId { get; set; } 
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
        public ICollection<Guid>? Images { get; set; }
        public string? ProductStatus { get; set; }
    }

    public class DeleteProductDto {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Guid ByUserId { get; set; }
    }

    public class GetProductDto
    {
    }
}
