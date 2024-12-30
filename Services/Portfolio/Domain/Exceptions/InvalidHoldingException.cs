using System;

namespace PortfolioService.Domain.Exceptions
{
    public class InvalidHoldingException : Exception
    {
        public InvalidHoldingException(string message) : base(message) { }
    }
}
