# Event-Driven Shopping System

This project is an event-driven shopping system built with C# and .NET 8. It demonstrates the use of event-driven architecture to manage product purchases, payments, inventory updates, and notifications.

## Features
- **Product Management**: Add, retrieve, and manage product inventory.
- **Purchase Service**: Interactive console-based product selection and purchase process.
- **Event Handling**: Publish and subscribe to events like payments, inventory updates, and notifications.
- **Extensibility**: Easily extendable with new event types and services.

## Technologies
- C# 12.0
- .NET 8
- Event-driven architecture

## How It Works
1. Users select products and specify purchase quantities.
2. Events like `PaymentEvent` and `WarehouseEvent` are published to handle payments and inventory updates.
3. Notifications are sent for successful transactions.

This project serves as a foundation for building scalable, event-driven systems.
