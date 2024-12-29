using Microsoft.EntityFrameworkCore;
using PaymentService.Data;

namespace PaymentService.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PaymentDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }
    }
}
