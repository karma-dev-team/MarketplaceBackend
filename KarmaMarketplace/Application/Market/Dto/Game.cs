using KarmaMarketplace.Application.Files.Dto;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Domain.Market.Enums;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.Market.Dto
{
    public class CreateGameDto 
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public GameTypes Type { get; set; }
        [Required]
        public List<string> Tags { get; set; } = null!;
        [Required]
        public CreateFileDto Logo { get; set; } = null!;
        [Required]
        public CreateFileDto Banner { get; set; } = null!;
    }

    public class DeleteGameDto 
    {
        [Required]
        public Guid GameId { get; set; }
    }

    public class UpdateGameDto 
    {
        [Required]
        public Guid GameId { get; set; }
        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string? Type { get; set; } = null!;
        public List<string>? Tags { get; set; } = null!;
        public CreateFileDto? Logo { get; set; } = null!;
        public CreateFileDto? Banner { get; set; } = null!;
    }

    public class GetGameDto
    {
        public Guid? GameId { get; set; }
        public string? Name { get; set; }
    }

    public class GetGamesListDto
    {
        public GameTypes? Type { get; set; }
        public string? Name { get; set; }
    }
}
