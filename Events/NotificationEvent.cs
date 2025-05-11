using EventDriven.Database;

namespace EventDriven.Events
{
    public class NotificationEvent
    {
        public ProductEntity Product { get; set; }
        public DateTime Timestamp { get; set; }

        public NotificationEvent(ProductEntity product)
        {
            Product = product;
            Timestamp = DateTime.UtcNow.AddHours(-6);
        }
    }
}