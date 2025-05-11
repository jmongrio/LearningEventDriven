using EventDriven.EventManager;
using EventDriven.Events;

namespace EventDriven.Services
{
    public class NotificationService
    {
        private readonly IEventBus _eventBus;

        public NotificationService(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<NotificationEvent>(HandleNotificationEvent);
        }

        private void HandleNotificationEvent(NotificationEvent notificationEvent)
        {
            Console.WriteLine($"[NotificationService] Mensaje recibido: {notificationEvent.Product.Description} en {notificationEvent.Timestamp}");
        }
    }
}
