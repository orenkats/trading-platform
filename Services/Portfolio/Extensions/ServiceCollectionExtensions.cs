using PortfolioService.Data.Repositories;
using PortfolioService.EventHandlers;
using PortfolioService.Logic;
using Shared.Messaging;
using Shared.Events;

namespace PortfolioService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        }

        public static void AddLogic(this IServiceCollection services)
        {
            services.AddScoped<IPortfolioLogic, PortfolioLogic>();
        }

        public static void AddEventHandlers(this IServiceCollection services)
        {
            services.AddScoped<IEventHandler<OrderPlacedEvent>, OrderPlacedEventHandler>();
            services.AddScoped<IEventHandler<UserCreatedEvent>, UserCreatedEventHandler>();
        }
    }
}
