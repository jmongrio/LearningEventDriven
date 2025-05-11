using EventDriven.Database;
using EventDriven.EventManager;
using EventDriven.Events;

namespace EventDriven.Services
{
    public class BuyService
    {
        private readonly IEventBus _eventBus;

        public BuyService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void ShowWindows()
        {
            bool exit = false;

            do
            {                
                ProductEntity? product = new();

                int selectedIndex = 0;

                ConsoleKey key;

                do
                {
                    product = new();
                    var products = BusinessData.GetProducts();                    

                    Console.Clear();
                    Console.WriteLine("Bienvenido al servicio de compra");

                    for (int i = 0; i < products.Count; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"> {products[i].Description} => {BusinessData.CurrencySimbol}{products[i].Price} => {products[i].Inventory}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"  {products[i].Description} => {BusinessData.CurrencySimbol}{products[i].Price} => {products[i].Inventory}");
                        }
                    }

                    key = Console.ReadKey(true).Key;

                    if(key == ConsoleKey.Escape)
                    {
                        exit = true;
                        break;
                    }

                    if (key == ConsoleKey.UpArrow)
                    {
                        selectedIndex = (selectedIndex == 0) ? products.Count - 1 : selectedIndex - 1;
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        selectedIndex = (selectedIndex == products.Count - 1) ? 0 : selectedIndex + 1;
                    }

                    Console.Clear();

                    product = products[selectedIndex];

                } while (key != ConsoleKey.Enter);

                Console.WriteLine($"¿Cuántos {product.Description} desea comprar? (0 para cancelar)");
                string? amount = Console.ReadLine();
                product.Inventory = int.Parse(amount ?? "0");

                bool stillBuy =
                        BusinessData.YesOrNoAnswer($"¿Está seguro de comprar el producto {product.Description} por {product.Price * product.Inventory}?");

                if (!stillBuy)
                {
                    exit = true;
                }

                _eventBus.Publish(new PaymentEvent(product));

            } while (!exit);
        }
    }
}