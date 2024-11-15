namespace Automated_Report_GenerationWorkerService
{
    public class AutomatedReportGenerator : BackgroundService
    {
        private readonly ILogger<AutomatedReportGenerator> _logger;
        private readonly DatabaseService _databaseService;
        private readonly FileService _fileService;
        private readonly TimeSpan _reportInterval = TimeSpan.FromDays(1); // Run daily

        public AutomatedReportGenerator(ILogger<AutomatedReportGenerator> logger, DatabaseService databaseService, FileService fileService)
        {
            _logger = logger;
            _databaseService = databaseService;
            _fileService = fileService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ReportGeneratorService is starting.");

            // Main loop
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Starting report generation at {Time}.", DateTime.Now);

                try
                {
                    // Fetch data
                    var reportData = _databaseService.FetchDataForReport();

                    // Generate report
                    var reportContent = GenerateReport(reportData);

                    // Save report to file
                    await _fileService.SaveReportAsync(reportContent, DateTime.Now);

                    _logger.LogInformation("Report generated successfully at {Time}.", DateTime.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while generating report.");
                }

                // Wait for the next interval
                await Task.Delay(_reportInterval, stoppingToken);
            }

            _logger.LogInformation("ReportGeneratorService is stopping.");
        }

        private string GenerateReport(string reportData)
        {
            // Generate report content as a simple string for demonstration
            return $"Report generated on {DateTime.Now}\n\n{reportData}";
        }


    }
}
