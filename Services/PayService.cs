using EventDriven.Database;
using EventDriven.EventManager;
using EventDriven.Events;

namespace EventDriven.Services
{
    public class PayService
    {
        private readonly IEventBus _eventBus;

        public PayService(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<PaymentEvent>(HandlePaymentEvent);
        }

        public void ShowWindows()
        {
            bool exit = false;
            do
            {
                var transactionHistories = BusinessData.GetTransactionHistories();

                Console.Clear();
                Console.WriteLine("Bienvenido al servicio de pago");

                Console.WriteLine("---------");
                Console.WriteLine($"Total de transacciones {transactionHistories.Count}");
                Console.WriteLine($"Total de pagado {BusinessData.CurrencySimbol}{transactionHistories.Sum(x => x.Price)}");
                Console.WriteLine("---------");


                for (int i = 0; i < transactionHistories.Count; i++)
                {
                    Console.WriteLine($"  {transactionHistories[i].Description} => {BusinessData.CurrencySimbol}{transactionHistories[i].Price} => {transactionHistories[i].Timestamp}");
                }

                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey(true);
                exit = true;
            }
            while (!exit);
        }

        private void HandlePaymentEvent(PaymentEvent paymentEvent)
        {
            TransactionHistory transaction = new()
            {
                Description = paymentEvent.Product.Description,
                Price = paymentEvent.Product.Price * paymentEvent.Product.Inventory,
                Timestamp = paymentEvent.Timestamp,
            };

            BusinessData.AddTransactionHistory(transaction);
            _eventBus.Publish(new WarehouseEvent(paymentEvent.Product));
            Console.WriteLine($"[PayService] Mensaje recibido: {paymentEvent.Product.Description} en {paymentEvent.Timestamp}");
        }
    }
}