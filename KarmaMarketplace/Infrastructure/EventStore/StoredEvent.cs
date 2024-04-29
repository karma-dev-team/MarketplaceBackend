using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Infrastructure.EventSourcing
{
    public class StoredEvent
    {
        [Key]
        public Guid EventId { get; set; }
        public Guid? ByUserId { get; set; }
        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; 
        [Required]
        public string EventType { get; set; } = null!;
        [Column(TypeName = "jsonb")]
        [Required]
        public string Data { get; set; } = null!; 
    }
}
