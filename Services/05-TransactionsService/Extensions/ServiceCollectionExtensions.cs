using TransactionsService.Data.Repositories;
using TransactionsService.EventHandlers;
using Shared.Messaging;


namespace TransactionsService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }

        // public static void AddLogic(this IServiceCollection services)
        // {
        //     services.AddScoped<ITransactionLogic, TransactionLogic>();
        // }

        public static void AddEventHandlers(this IServiceCollection services)
        {
            services.AddScoped<IEventHandler<object>, FundsEventHandler>();
            
        }
    }
}
