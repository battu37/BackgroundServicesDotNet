using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automated_Report_GenerationWorkerService
{
    public class FileService
    {
        private readonly string _reportDirectory = @"C:\Reports";

        public FileService()
        {
            // Ensure the directory exists
            if (!Directory.Exists(_reportDirectory))
            {
                Directory.CreateDirectory(_reportDirectory);
            }
        }

        public async Task SaveReportAsync(string reportContent, DateTime reportDate)
        {
            var fileName = Path.Combine(_reportDirectory, $"Report_{reportDate:yyyyMMdd}.txt");

            // Write report to file
            await File.WriteAllTextAsync(fileName, reportContent);

            Console.WriteLine($"Report saved as {fileName}");
        }

    }
}
