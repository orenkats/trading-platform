using PaymentService.Data.Repositories;
using Shared.Messaging;
using Shared.Events;

namespace PaymentService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPaymentRepository, PaymentRepository>();
        }

        // public static void AddLogic(this IServiceCollection services)
        // {
        //     services.AddScoped<IPaymentLogic, PaymentLogic>();
        // }

        // public static void AddEventHandlers(this IServiceCollection services)
        // {
        //     services.AddScoped<IEventHandler<OrderPlacedEvent>, OrderPlacedEventHandler>();
        //     services.AddScoped<IEventHandler<UserCreatedEvent>, UserCreatedEventHandler>();
        // }
    }
}
