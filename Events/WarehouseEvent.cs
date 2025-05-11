using EventDriven.Database;

namespace EventDriven.Events
{
    public class WarehouseEvent
    {
        public ProductEntity Product { get; set; }
        public DateTime Timestamp { get; set; }

        public WarehouseEvent(ProductEntity product)
        {
            Product = product;
            Timestamp = DateTime.UtcNow.AddHours(-6);
        }
    }
}