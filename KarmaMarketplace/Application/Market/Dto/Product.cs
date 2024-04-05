using KarmaMarketplace.Application.Common.Models;
using KarmaMarketplace.Application.Files.Dto;
using KarmaMarketplace.Application.Files.UseCases;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Domain.Market.ValueObjects;
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
        public ICollection<CreateFileDto> Images { get; set; } = [];
        public ICollection<string> AutoAnswers { get; set; } = []; 
    }

    public class UpdateProductDto
    {
        [Required]
        public Guid ProductId { get; set; }
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
        public ICollection<Guid>? Images { get; set; }
        public string? ProductStatus { get; set; }
        public ICollection<string>? AutoAnswers { get; set; }
    }

    public class DeleteProductDto {
        [Required]
        public Guid ProductId { get; set; }
    }

    public class GetProductDto
    {
        [Required]
        public Guid ProductId { get; set; } 
    }

    public class GetProductsListDto : InputPagination
    {
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }   
        public Guid? GameId { get; set; }
        public string? Status { get; set; } 
    }

    public class AnalyticsInformationDto {
        [Required]
        public int TotalViews { get; set; }
        [Required]
        public int ViewsInWeek { get; set; }
        [Required]
        public Money Revenue { get; set; } = null!; 

        public AnalyticsInformationDto(int totalViews, int viewsInWeek, Money revenue)
        {
            TotalViews = totalViews;
            ViewsInWeek = viewsInWeek;
            Revenue = revenue; 
        }
    }

    public class GetAnalyticsDto {  
        public Guid? ProductId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
