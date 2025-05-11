using EventDriven.EventManager;
using EventDriven.Services;
using EventDriven.Windows;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// Registrar EventBus como singleton (una única instancia compartida)
services.AddSingleton<IEventBus, EventBus>();

// Registrar los servicios como transitorios
services.AddTransient<NotificationService>();
services.AddTransient<PayService>();
services.AddTransient<WarehouseService>();
services.AddTransient<BuyService>();

// App windows
services.AddTransient<HomeWindows>();

// Construir el proveedor de servicios
var serviceProvider = services.BuildServiceProvider();

serviceProvider.GetRequiredService<NotificationService>();
serviceProvider.GetRequiredService<PayService>();
serviceProvider.GetRequiredService<WarehouseService>();

// Resolver los servicios
var buyService = serviceProvider.GetRequiredService<BuyService>();

var homeService = serviceProvider.GetRequiredService<HomeWindows>();

homeService.Home();