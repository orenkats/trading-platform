using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.DbContexts;

namespace OrderService.Infrastructure.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<OrderDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }
    }
}
