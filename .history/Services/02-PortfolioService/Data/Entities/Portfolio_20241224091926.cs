using System;

namespace PortfolioService.Data.Entities
{
    public class Portfolio
    {
        public Guid Id { get; set; } // Unique identifier for the portfolio entry.
        public Guid UserId { get; set; } // References the user who owns this portfolio.
        public string StockSymbol { get; set; } // The stock symbol from StockCatalog.

        public int TotalQuantity { get; set; } // Total number of shares owned.
        public decimal AveragePrice { get; set; } // Weighted average purchase price.
        public decimal CurrentValue { get; set; } // Current value of the holding.

        public DateTime LastUpdated { get; set; } // Tracks the last update to the portfolio entry.
    }
}
