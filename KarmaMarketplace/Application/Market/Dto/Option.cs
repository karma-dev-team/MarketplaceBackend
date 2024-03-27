using KarmaMarketplace.Domain.Market.Enums;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.Market.Dto
{
    public class CreateOptionDto
    {
        [Required, MaxLength(256)]
        public string? Group { get; set; } = null!;

        // Имя значения, тоесть то что будет показываться при выборе значении,
        // для SWITCH это имя значения
        [Required, MaxLength(256)]
        public string Label { get; set; } = null!;

        // Тип Опции, Свитч для True и False, Selector для выбора только одного атрибута, Range для цифры 
        [Required]
        public OptionTypes Type { get; set; }; 

        // Значение типо True, False, 1800
        [MaxLength(256)]
        public string Value { get; set; } = null!;

        // Репрезинтация атрибута как англиский аналог для разделения и перевода 
        [MaxLength(256)]
        public string Field { get; set; } = null!;

        // Очередь, типо 1, 2, 3 и т.д. 
        public int Sequence { get; set; }

        // Для RANGE цифр. 
        public int? ValueRangeMin { get; set; }

        public int? ValueRangeMax { get; set; }
    }
}
