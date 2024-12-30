using System;

namespace PortfolioService.Domain.Exceptions
{
    public class PortfolioAlreadyExistsException : Exception
    {
        public PortfolioAlreadyExistsException(Guid userId) : base($"Portfolio already exists for: {userId}")
        {
        }
    }
}
