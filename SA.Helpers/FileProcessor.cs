using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using SA.Entities;
using SA.Helpers.Interfaces;

namespace SA.Helpers
{
    public class FileProcessor : IFileProcessor
    {
        private readonly ILogger<FileProcessor> _logger;

        public FileProcessor(ILogger<FileProcessor> logger)
        {
            _logger = logger;
        }

        public Customer ExtractCustomerDataFromInputFile(string fileName)
        {
            _logger.LogDebug($"Reading the file: {fileName}");
            Customer customer = new Customer();

            //Read all Lines from the file.
            string[] records = File.ReadAllLines(fileName);

            if (records.Any())
            {
                //Retrieve the records and assign to the entity.
                customer.DriverInfo = records.Where(d => d.Split(' ').FirstOrDefault() == Constants.InputCommands.Driver.ToString()).ToList();
                customer.TripInfo = records.Where(d => d.Split(' ').FirstOrDefault() == Constants.InputCommands.Trip.ToString()).ToList();
            }
            else
            {
                //Throw an exception if the file do not have any contents.
                throw new Exception($"File {fileName} do not have any contents.");
            }
            return customer;
        }
    }
}
