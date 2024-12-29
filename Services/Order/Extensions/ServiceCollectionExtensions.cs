using OrderService.Data.Repositories;
using OrderService.Logic;
using Shared.Messaging;
using Shared.Events;

namespace OrderService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
        }

        public static void AddLogic(this IServiceCollection services)
        {
            services.AddScoped<IOrderLogic, OrderLogic>();
        }

        
    }
}
