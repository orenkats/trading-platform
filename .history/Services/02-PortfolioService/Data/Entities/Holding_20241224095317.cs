namespace PortfolioService.Data.Entities
{
    public class Holding
    {
        public Guid Id { get; set; } // Unique ID for the holding
        public string StockSymbol { get; set; } = string.Empty; // Stock symbol (e.g., NVDA)
        public int Quantity { get; set; } // Number of stocks held
        public decimal AveragePrice { get; set; } // Average purchase price
    }
}
