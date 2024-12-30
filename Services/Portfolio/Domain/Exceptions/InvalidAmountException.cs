using System;

namespace PortfolioService.Domain.Exceptions
{
    public class InvalidAmountException : Exception
    {
        public InvalidAmountException() : base("Amount must be greater than zero.") { }
    }
}
