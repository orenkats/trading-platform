using Microsoft.EntityFrameworkCore;
using PortfolioService.Infrastructure.Persistence;

namespace PortfolioService.Infrastructure.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<PortfolioDbContext>(options =>
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

                Console.WriteLine("Database connection successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection failed: {ex.Message}");
            }
        }
    }
}
