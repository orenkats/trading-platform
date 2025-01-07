using Shared.Events;
using Shared.Messaging;
using NotificationService.Domain.Entities;
using NotificationService.Infrastracture.Repositories;
using NotificationService.Domain.Interfaces;

namespace NotificationService.EventHandlers;

public class WithdrawalApprovedEventHandler : IEventHandler<WithdrawalApprovedEvent>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationDomainService _notificationDomainService;

    public WithdrawalApprovedEventHandler(INotificationRepository notificationRepository, INotificationDomainService notificationDomainService)
    {
        _notificationRepository = notificationRepository;
        _notificationDomainService = notificationDomainService;
    }

}