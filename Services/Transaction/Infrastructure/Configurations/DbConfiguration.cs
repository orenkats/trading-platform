using Microsoft.EntityFrameworkCore;
using TransactionService.Infrastructure.DbContexts;

namespace TransactionService.Infrastructure.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TransactionDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }
    }
}
