namespace EventDrivenOrderProcessor
{
    public class OrderProcessorService : BackgroundService
    {
        private readonly ILogger<OrderProcessorService> _logger;
        private readonly MessageQueueService _messageQueueService;
        private readonly DatabaseService _databaseService;

        public OrderProcessorService(ILogger<OrderProcessorService> logger, MessageQueueService messageQueueService, DatabaseService databaseService)
        {
            _logger = logger;
            _messageQueueService = messageQueueService;
            _databaseService = databaseService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("OrderProcessorService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var orderEvent = await _messageQueueService.ReceiveOrderEventAsync(stoppingToken);

                if (orderEvent != null)
                {
                    _logger.LogInformation("Received new order event: {OrderId}", orderEvent.OrderId);

                    // Process the order
                    try
                    {
                        await _databaseService.SaveOrderAsync(orderEvent);
                        _logger.LogInformation("Order {OrderId} processed and saved.", orderEvent.OrderId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to process order {OrderId}.", orderEvent.OrderId);
                    }
                }

                await Task.Delay(1000, stoppingToken); // Delay before checking for new messages
            }

            _logger.LogInformation("OrderProcessorService is stopping.");
        }
    }
}
