using System;

namespace PortfolioService.Domain.Exceptions
{
    public class PortfolioNotFoundException : Exception
    {
        public PortfolioNotFoundException(Guid userId) : base($"Portfolio not found for UserId: {userId}") { }
    }
}
