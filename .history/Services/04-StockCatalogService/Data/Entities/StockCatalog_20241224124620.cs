namespace StockCatalogService.Data.Entities
{
    public class StockCatalog
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier
        public string StockSymbol { get; set; } = string.Empty; // Stock symbol
    }
}
