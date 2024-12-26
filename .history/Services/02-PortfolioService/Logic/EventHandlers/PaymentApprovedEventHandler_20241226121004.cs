using PortfolioService.Data.Entities;
using Shared.Events;

namespace PortfolioService.Logic.EventHandlers
{
    public class PaymentApprovedEventHandler
    {
        private readonly IPortfolioLogic _portfolioLogic;

        public PaymentApprovedEventHandler(IPortfolioLogic portfolioLogic)
        {
            _portfolioLogic = portfolioLogic;
        }

        public async Task HandlePaymentApprovedEventAsync(PaymentApprovedEvent PaymentApprovedEvent)
        {
            
        }
    }
}