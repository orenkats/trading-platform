using NotificationsService.Data.Repositories;
using NotificationsService.EventHandlers;
using NotificationsService.Logic;
using Shared.Messaging;
using Shared.Events;

namespace NotificationsService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<INotificationRepository, NotificationRepository>();
        }

        public static void AddLogic(this IServiceCollection services)
        {
            services.AddScoped<INotificationLogic, NotificationLogic>();
        }

        public static void AddEventHandlers(this IServiceCollection services)
        {
            services.AddScoped<IEventHandler<object>, FundsEventHandler>();
            services.AddScoped<IEventHandler<UserCreatedEvent>, UserCreatedEventHandler>();
            
        }
    }
}
