using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDrivenOrderProcessor
{
    public class DatabaseService
    {
        public Task SaveOrderAsync(OrderEvent orderEvent)
        {
            // Simulate saving to a database
            Console.WriteLine($"Order saved: {orderEvent.OrderId}, Product: {orderEvent.ProductName}, Quantity: {orderEvent.Quantity}");
            return Task.CompletedTask;
        }

    }
}
