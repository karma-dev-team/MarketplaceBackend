using KarmaMarketplace.Domain.Market.Enums;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Market.Entities
{
    public class OptionEntity : BaseAuditableEntity
    {
        [Required, MaxLength(256)]
        public string Group { get; set; } = null!; 

        [Required, MaxLength(256)]
        public string Label { get; set; } = null!;

        public OptionTypes Type { get; set; }

        [MaxLength(256)]
        public string Value { get; set; } = null!;

        [MaxLength(256)]
        public string Field { get; set; } = null!;

        public int Sequence { get; set; }

        public int? ValueRangeMin { get; set; }

        public int? ValueRangeMax { get; set; }
    }
}
