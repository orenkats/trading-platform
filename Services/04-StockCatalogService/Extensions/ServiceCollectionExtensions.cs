using StockCatalogService.Data.Repositories;
using StockCatalogService.Logic;
using Shared.Messaging;
using Shared.Events;

namespace StockCatalogService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStockCatalogRepository, StockCatalogRepository>();
        }

        public static void AddLogic(this IServiceCollection services)
        {
            services.AddScoped<IStockCatalogLogic, StockCatalogLogic>();
        }

        
    }
}
