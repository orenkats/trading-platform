using Microsoft.Extensions.DependencyInjection;
using PortfolioService.Application.EventHandlers;
using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Application.Extensions
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // Register Event Handlers
            services.AddScoped<IEventHandler<UserCreatedEvent>, UserCreatedEventHandler>();
            services.AddScoped<IEventHandler<OrderPlacedEvent>, OrderPlacedEventHandler>();

            return services;
        }
    }
}
