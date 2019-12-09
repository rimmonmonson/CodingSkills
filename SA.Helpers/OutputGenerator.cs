using Microsoft.Extensions.Logging;
using SA.Entities;
using SA.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Helpers
{
    public class OutputGenerator : IOutputGenerator
    {
        private readonly ILogger<OutputGenerator> _logger;
        public OutputGenerator(ILogger<OutputGenerator> logger)
        {
            _logger = logger;
        }
        public void PrintOutput(List<Driver> drivers)
        {
            _logger.LogDebug("Printing Report Output to Console.");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Report Output:");

            foreach (var driver in drivers.OrderByDescending(t => t.TotalDistance))
            {
                sb.Append($"{driver.Name}: { Convert.ToInt32(driver.TotalDistance) } miles");
                sb.AppendLine(driver.AverageSpeed > 0 ? $" @ { Convert.ToInt32(driver.AverageSpeed) } mph" : string.Empty);
            }
            Console.WriteLine(sb);
        }
    }
}
