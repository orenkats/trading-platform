using Microsoft.Extensions.DependencyInjection;
using PortfolioService.Domain.Interfaces;
using PortfolioService.Domain.Services;

namespace PortfolioService.Domain.Extensions
{
    public static class DomainServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainLayer(this IServiceCollection services)
        {
            // Register Domain Services
            services.AddScoped<IPortfolioDomainService, PortfolioDomainService>();

            return services;
        }
    }
}
