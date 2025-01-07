using Shared.Events;
using Shared.Messaging;
using NotificationService.Domain.Entities;
using NotificationService.Infrastracture.Repositories;
using NotificationService.Domain.Interfaces;

namespace NotificationsService.EventHandlers;

public class DepositCompletedEventHandler : IEventHandler<DepositCompletedEvent>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationDomainService _notificationDomainService;

    public DepositCompletedEventHandler(INotificationRepository notificationRepository, INotificationDomainService notificationDomainService)
    {
        _notificationRepository = notificationRepository;
        _notificationDomainService = notificationDomainService;
    }

}