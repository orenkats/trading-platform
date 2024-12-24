using System;
using System.Collections.Generic;

namespace PortfolioService.Data.Entities
{
    public class Portfolio
    {
        public Guid Id { get; set; } // Unique ID for the Portfolio
        public Guid UserId { get; set; } // Relationship with the User
        public List<Holding> Holdings { get; set; } = new List<Holding>(); // List of Holdings
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Portfolio creation time
    }

    public class Holding
    {
        public Guid PortfolioId { get; set; } // Foreign key referencing Portfolio
        public string StockSymbol { get; set; } = string.Empty; // Stock Symbol (e.g., "AAPL")
        public int Quantity { get; set; } // Number of shares
        public decimal AveragePrice { get; set; } // Average purchase price of the shares

        // Navigation property to the Portfolio
        public Portfolio Portfolio { get; set; } = null!;
    }
}
