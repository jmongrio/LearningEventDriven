using EventDriven.Database;

namespace EventDriven.Events
{
    public class PaymentEvent
    {
        public ProductEntity Product { get; set; }
        public DateTime Timestamp { get; set; }

        public PaymentEvent(ProductEntity product)
        {
            Product = product;
            Timestamp = DateTime.UtcNow.AddHours(-6);
        }
    }
}