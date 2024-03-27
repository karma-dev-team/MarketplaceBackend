using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.Market.Dto
{
    public class CreateCategoryDto 
    {
        [Required]
        public string Name { get; set; } = null!;
        public ICollection<CreateOptionDto> Options { get; set; } = []; 
        public Guid GameId { get; set; }
    }

    public class GetCategoryDto {
        [Required]
        public Guid CategoryId { get; set; }
    }

    public class GetCategoriesListDto
    {
        public string Name { get; set; } = null!; 
    }

    public class DeleteCategoryDto
    {
        [Required]
        public Guid CategoryId { get; set; }
    }

    public class UpdateCategoryDto {
        [Required]
        public Guid CategoryId { get; set; }
        public string? Name { get; set; }
        public ICollection<CreateOptionDto> Options { get; set; } = []; 
    }
}
