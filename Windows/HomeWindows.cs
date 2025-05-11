using EventDriven.Services;

namespace EventDriven.Windows
{
    public class HomeWindows
    {
        private readonly BuyService _buyService;
        private readonly WarehouseService _warehouseService;
        private readonly PayService _payService;

        public HomeWindows(
            BuyService buyService, 
            WarehouseService warehouseService,
            PayService payService)
        {
            _buyService = buyService;
            _warehouseService = warehouseService;
            _payService = payService;
        }

        public void Home()
        {
            bool exit = false;

            do
            {
                string[] windows = ["Compra", "Transacciones", "Inventario", "Salir"];
                int selectedIndex = 0;

                ConsoleKey key;

                do
                {
                    Console.Clear();

                    Console.WriteLine("Bienvenido");

                    for (int i = 0; i < windows.Length; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"> {windows[i]}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"  {windows[i]}");
                        }
                    }

                    key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow)
                    {
                        selectedIndex = (selectedIndex == 0) ? windows.Length - 1 : selectedIndex - 1;
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        selectedIndex = (selectedIndex == windows.Length - 1) ? 0 : selectedIndex + 1;
                    }
                }
                while (key != ConsoleKey.Enter);

                Console.Clear();

                switch (selectedIndex)
                {
                    case 0:
                        _buyService.ShowWindows();
                        break;
                    case 1:
                        _payService.ShowWindows();
                        break;
                    case 2:
                        _warehouseService.ShowWindow();
                        break;
                    case 3:
                        exit = true;
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        exit = true;
                        Console.WriteLine("Saliendo...");
                        break;
                }
            } while (!exit);
        }
    }
}