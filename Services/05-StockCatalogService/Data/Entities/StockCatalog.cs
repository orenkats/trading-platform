using System;

namespace StockCatalogService.Data.Entities
{
    public class StockCatalog
    {
        public Guid Id { get; set; } // Unique identifier for each stock
        public string StockSymbol { get; set; } = null!; // Stock ticker symbol
        public decimal Price { get; set; } // Price of the stock
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for when the stock is added
    }
}
