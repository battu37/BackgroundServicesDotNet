using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDrivenOrderProcessor
{
    public class MessageQueueService
    {
        public async Task<OrderEvent> ReceiveOrderEventAsync(CancellationToken cancellationToken)
        {
            // Simulate a delay for receiving a new order event
            await Task.Delay(500, cancellationToken);

            // Simulate receiving a new order event
            return new OrderEvent
            {
                OrderId = Guid.NewGuid(),
                ProductName = "Sample Product",
                Quantity = new Random().Next(1, 5),
                OrderDate = DateTime.Now
            };
        }

    }

    public class OrderEvent
    {
        public Guid OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }


}
