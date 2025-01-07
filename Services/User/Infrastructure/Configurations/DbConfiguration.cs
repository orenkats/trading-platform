using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.DbContexts;

namespace UserService.Infrastructure.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UserDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }
    }
}
