using UserService.Data.Repositories;
using UserService.Logic;
using Shared.Messaging;
using Shared.Events;

namespace UserService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void AddLogic(this IServiceCollection services)
        {
            services.AddScoped<IUserLogic, UserLogic>();
        }

    }
}
