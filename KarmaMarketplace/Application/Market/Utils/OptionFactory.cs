using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;

namespace KarmaMarketplace.Application.Market.Utils
{
    public static class OptionFactory
    {
        public static ICollection<OptionEntity> CreateOptions(ICollection<CreateOptionDto> options)
        {
            List<OptionEntity> Options = [];

            foreach (var option in options)
            {
                if (option.Type == Domain.Market.Enums.OptionTypes.Selector)
                {
                    Guard.Against.Null(option.Group, message: "Group for option is not provided");

                    Options.Add(OptionEntity.CreateSelector(
                        value: option.Value,
                        label: option.Label,
                        group: option.Group,
                        field: option.Field,
                        sequence: option.Sequence));
                }
                if (option.Type == Domain.Market.Enums.OptionTypes.Switch)
                {
                    Options.Add(OptionEntity.CreateSwitch(
                        label: option.Label,
                        field: option.Field,
                        sequence: option.Sequence));
                }
                if (option.Type == Domain.Market.Enums.OptionTypes.Range)
                {
                    Guard.Against.Null(option.ValueRangeMin, message: "Value range min is null");
                    Guard.Against.Null(option.ValueRangeMax, message: "Value range max is null");

                    Options.Add(OptionEntity.CreateRange(
                        label: option.Label,
                        value: option.Value,
                        field: option.Field,
                        sequence: option.Sequence,
                        min: (int)option.ValueRangeMin,
                        max: (int)option.ValueRangeMax));
                }
            }

            return Options; 
        }
    }
}
