using System;

namespace PortfolioService.Domain.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        private const string DefaultMessage = "Insufficient funds for this withdrawal.";

        public InsufficientFundsException() : base(DefaultMessage)
        {
        }

        public InsufficientFundsException(string message) : base(message)
        {
        }
    }
}
