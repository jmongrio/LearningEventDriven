using System.Diagnostics;

namespace EventDriven.Database
{
    public static class BusinessData
    {
        public static string CurrencySimbol = "$";

        public static List<ProductEntity> Products { get; set; } = new List<ProductEntity>
        {
            new ProductEntity
            {
                Inventory = 50,
                Description = "Laptop de alta gama",
                Price = 1500.99m
            },
            new ProductEntity
            {
                Inventory = 100,
                Description = "Teclado mecánico",
                Price = 89.99m
            },
            new ProductEntity
            {
                Inventory = 200,
                Description = "Mouse inalámbrico",
                Price = 29.99m
            },
            new ProductEntity
            {
                Inventory = 75,
                Description = "Monitor 4K",
                Price = 399.99m
            },
            new ProductEntity
            {
                Inventory = 150,
                Description = "Auriculares con cancelación de ruido",
                Price = 199.99m
            }
        };

        #region Warehouse
        public static List<WarehouseRecords> Warehouse { get; set; } = new List<WarehouseRecords>();

        public static void AddWarehouseRecord(WarehouseRecords warehouse)
        {
            if (Warehouse.Any(w => w.Id == warehouse.Id))
            {
                Debug.WriteLine($"Warehouse with ID {warehouse.Id} already exists.");
                return;
            }
            Warehouse.Add(warehouse);
            Debug.WriteLine($"Warehouse with ID {warehouse.Id} added.");
        }

        public static List<WarehouseRecords> GetWarehouses()
        {
            return Warehouse;
        }
        #endregion

        #region Transaction History
        public static List<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();

        public static void AddTransactionHistory(TransactionHistory transactionHistory)
        {
            TransactionHistories.Add(transactionHistory);
            Debug.WriteLine($"Transaction history with ID {transactionHistory.Id} added.");
        }

        public static List<TransactionHistory> GetTransactionHistories()
        {
            return TransactionHistories;
        }
        #endregion

        #region Products
        public static void AddProduct(ProductEntity product)
        {
            if (Products.Any(p => p.Id == product.Id))
            {
                Debug.WriteLine($"Product with ID {product.Id} already exists.");
                return;
            }
            Products.Add(product);
            Debug.WriteLine($"Product with ID {product.Id} added.");
        }

        public static List<ProductEntity> GetProducts()
        {
            return Products;
        }

        public static ProductEntity? GetProduct(string id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public static void UpdateExitsingProduct(string id, int amount, INVENTORY_ACTION warehouseAction)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                switch (warehouseAction)
                {
                    case INVENTORY_ACTION.ADD:
                        product.Inventory += amount;
                        break;
                    case INVENTORY_ACTION.REMOVE:
                        product.Inventory -= amount;
                        break;
                    default:
                        break;
                }
                                
                Debug.WriteLine($"Product with ID {id} updated.");
            }
            Debug.WriteLine($"Product with ID {id} not found.");
        }
        #endregion

        public static bool YesOrNoAnswer(string message)
        {
            string[] answers = { "Sí", "No" };
            int selectedIndex = 1;

            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine(message);

                for (int i = 0; i < answers.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {answers[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {answers[i]}");
                    }
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex == 0) ? answers.Length - 1 : selectedIndex - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex == answers.Length - 1) ? 0 : selectedIndex + 1;
                }

            } while (key != ConsoleKey.Enter);

            if (selectedIndex == 0)
            {
                return true;
            }

            Console.Clear();

            return false;
        }
    }

    public class ProductEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Inventory { get; set; } = 0;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; } = decimal.Zero;

        public ProductEntity() { }

        public ProductEntity(int inventoty, string description, decimal price)
        {
            Id = Guid.NewGuid().ToString();
            Inventory = inventoty;
            Description = description;
            Price = price;
        }
    }

    public class TransactionHistory
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; } = null!;
        public decimal Price { get; set; } = decimal.Zero;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class WarehouseRecords
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ProductId { get; set; } = null!;
        public INVENTORY_ACTION Action { get; set; }
        public int Amount { get; set; }
    }

    public enum INVENTORY_ACTION
    {
        ADD,
        REMOVE
    }
}