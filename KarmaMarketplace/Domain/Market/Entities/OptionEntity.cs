using KarmaMarketplace.Domain.Market.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace KarmaMarketplace.Domain.Market.Entities
{
    public class OptionEntity : BaseAuditableEntity
    {
        // К какой группе относится опция, полезно когда их много. 
        [Required, MaxLength(256)]
        public string Group { get; set; } = null!; 

        // Имя значения, тоесть то что будет показываться при выборе значении,
        // для SWITCH это имя значения
        [Required, MaxLength(256)]
        public string Label { get; set; } = null!;

        // Тип Опции, Свитч для True и False, Selector для выбора только одного атрибута, Range для цифры 
        [Required]
        public OptionTypes Type { get; set; }

        // Значение типо True, False, 1800
        [MaxLength(256), Required]
        public string Value { get; set; } = null!;

        // Репрезинтация атрибута как англиский аналог для разделения и перевода 
        [MaxLength(256), Required]
        public string Field { get; set; } = null!;

        // Очередь, типо 1, 2, 3 и т.д. 
        [Required]
        public int Sequence { get; set; }

        // Для RANGE цифр. 
        public int? ValueRangeMin { get; set; }
        public int? ValueRangeMax { get; set; }

        public static OptionEntity CreateRange(
            string label,
            string value,
            string field,
            int sequence,
            int min, 
            int max)
        {
            var range = new OptionEntity();

            range.Label = label;
            range.Value = value;
            range.Field = field;
            range.Sequence = sequence;
            range.Type = OptionTypes.Range;
            range.ValueRangeMin = min;
            range.ValueRangeMax = max; 

            return range;
        }

        public static OptionEntity CreateSwitch(
            string label, 
            string field, 
            int sequence)
        {
            var option = new OptionEntity();

            option.Label = label;
            option.Type = OptionTypes.Switch; 
            option.Field = field;
            option.Sequence = sequence; 
            
            return option;
        }

        public static OptionEntity CreateSelector(string label, string value, string group, string field, int sequence)
        {
            return new OptionEntity
            {
                Label = label,
                Value = value,
                Field = field,
                Group = group,
                Sequence = sequence,
                Type = OptionTypes.Selector
            };
        }
    }
}
