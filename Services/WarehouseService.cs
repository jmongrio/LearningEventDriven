using EventDriven.Database;
using EventDriven.EventManager;
using EventDriven.Events;

namespace EventDriven.Services
{
    public class WarehouseService
    {
        private readonly IEventBus _eventBus;

        public WarehouseService(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<WarehouseEvent>(HandleWarehouseEvent);
        }

        public void ShowWindow()
        {
            bool exit = false;
            do
            {
                var products = BusinessData.Products;
                Console.Clear();
                Console.WriteLine("Bienvenido al servicio de inventario");
                Console.WriteLine("---------");
                Console.WriteLine($"Total de productos {products.Count}");
                Console.WriteLine("---------");
                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine($"  {products[i].Description} => {products[i].Inventory}");
                }
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey(true);
                exit = true;
            }
            while (!exit);
        }

        private void HandleWarehouseEvent(WarehouseEvent warehouseEvent)
        {
            WarehouseRecords warehouseRecords = new()
            {
                ProductId = warehouseEvent.Product.Id,
                Action = INVENTORY_ACTION.REMOVE,
                Amount = warehouseEvent.Product.Inventory,
            };

            BusinessData.AddWarehouseRecord(warehouseRecords);
            BusinessData.UpdateExitsingProduct(warehouseEvent.Product.Id, warehouseEvent.Product.Inventory, INVENTORY_ACTION.REMOVE);
            _eventBus.Publish(new NotificationEvent(warehouseEvent.Product));
            Console.WriteLine($"[WarehouseService] Mensaje recibido: {warehouseEvent.Product.Description} en {warehouseEvent.Timestamp}");
        }
    }
}
