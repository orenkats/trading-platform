namespace PortfolioService.Application.DTOs;

public class PortfolioResponse
{
    public Guid UserId { get; set; }
    public decimal AccountBalance { get; set; }
    public List<HoldingResponse> Holdings { get; set; } = new();

    public class HoldingResponse
    {
        public string StockSymbol { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal AveragePrice { get; set; }
    }
}
