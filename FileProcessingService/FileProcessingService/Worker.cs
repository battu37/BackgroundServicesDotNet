namespace FileProcessingService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly string _directoryPath = @"C:\MonitoredFolder";

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Ensure the directory exists
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
                _logger.LogInformation("Created directory: {Directory}", _directoryPath);
            }

            _logger.LogInformation("Monitoring directory: {Directory}", _directoryPath);

            // Continuously monitor directory until the service is stopped
            while (!stoppingToken.IsCancellationRequested)
            {
                var files = Directory.GetFiles(_directoryPath);
                foreach (var file in files)
                {
                    try
                    {
                        // Process each file (e.g., parse the file and upload data to a database)
                        await ProcessFile(file);

                        // Delete the file after processing
                        File.Delete(file);
                        _logger.LogInformation("Processed and deleted file: {FileName}", file);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing file: {FileName}", file);
                    }
                }

                // Wait for 5 seconds before checking for new files again
                await Task.Delay(5000, stoppingToken);
            }
        }

        private Task ProcessFile(string file)
        {
            // Simulate file processing
            _logger.LogInformation("Processing file: {FileName}", file);
            // Here you would add logic to parse the file, interact with the database, etc.
            return Task.CompletedTask;
        }
    }
}
