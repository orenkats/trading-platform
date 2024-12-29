namespace PortfolioService.DTOs
{
    public class PortfolioDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal AccountBalance { get; set; }
        public List<HoldingDto> Holdings { get; set; } = new List<HoldingDto>();
    }

    public class HoldingDto
    {
        public string StockSymbol { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal AveragePrice { get; set; }
    }
}
