

namespace Shared.Events
{
    public class OrderPlacedEvent : IEvent
    {
        public Guid EventId { get; set; } = Guid.NewGuid(); // Unique identifier for the event
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp when the event was created

        public Guid OrderId { get; set; } // Unique identifier for the order
        public Guid UserId { get; set; } // User who placed the order
        public string StockSymbol { get; set; } = null!; // Stock symbol of the ordered stock
        public int Quantity { get; set; } // Number of stocks ordered
        public decimal Price { get; set; } // Price per stock
    }
}
