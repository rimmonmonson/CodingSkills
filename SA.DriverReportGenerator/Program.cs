using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SA.Helpers;
using SA.Helpers.Interfaces;

namespace SA.DriverReportGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            #region ConfigureServices
            var services = new ServiceCollection()
            .AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            })
           .AddSingleton<IFileProcessor, FileProcessor>()
           .AddSingleton<IHelper, Helper>()
           .AddSingleton<IOutputGenerator, OutputGenerator>()
           .BuildServiceProvider();
            #endregion

            #region ConfigureLogging
            var logger = services.GetService<ILoggerFactory>()
            .CreateLogger<Program>();
            #endregion

            IFileProcessor processor = Factory.GetFileProcessor(services);
            IHelper helper = Factory.GetHelper(services);


            logger.LogDebug("Driver Report Generator [Start]");

            try
            {
                Console.WriteLine("Enter File Path:");
                var filePath = Console.ReadLine();

                if (File.Exists(filePath))
                {
                    //Extract Customer Details from FilePath
                    var customerData = processor.ExtractCustomerDataFromInputFile(filePath);

                    //Retrieve Driver Info
                    var drivers = helper.RetrieveDriverInfo(customerData.DriverInfo);

                    //Capture Trip Info
                    helper.CaptureTripInfo(customerData.TripInfo, drivers);

                    //CalculateDuration based on Trips
                    helper.CalculateDuration(drivers);

                    //Generate Output in Console.
                    IOutputGenerator outputGenerator = Factory.GetOutputGenerator(services);
                    outputGenerator.PrintOutput(drivers);
                }
                else
                {
                    throw new Exception($"File {filePath} not found or does not exists.");
                }
            }
            catch (global::System.Exception ex)
            {
                logger.LogError($"Unhandled Exception occured. Error Message: {ex.Message}", ex);
            }
            logger.LogDebug("Driver Report Generator [End]");
        }

    }
}
