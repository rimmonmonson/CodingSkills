using SA.Entities;
using SA.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace SA.Helpers
{
    public class Helper : IHelper
    {
        private readonly ILogger<Helper> _logger;

        public Helper(ILogger<Helper> logger)
        {
            _logger = logger;
        }
        public List<Driver> RetrieveDriverInfo(List<string> driverInputLines)
        {
            List<Driver> drivers = new List<Driver>();
            Dictionary<string, string> uniqueDrivers = new Dictionary<string, string>();
            foreach (var line in driverInputLines)
            {
                var data = line.Split(' ');

                //To validate if the input feed has exact columns
                if (line.Length != 2)
                {
                    var driverName = data[1].Trim();
                    //Add only unique Drivers in the Driver Entity.
                    if (!uniqueDrivers.ContainsKey(driverName))
                    {
                        var driver = new Driver
                        {
                            Name = driverName
                        };

                        _logger.LogDebug($"Adding new new driver: {driverName}");
                        drivers.Add(driver);
                        uniqueDrivers.Add(driverName, driverName);
                    }
                    else
                    {
                        _logger.LogDebug($"Driver details already captured: {driverName}");
                    }
                }
                else
                {
                    _logger.LogError($"Driver Input file format is not correct on the line: {line}");
                    throw new Exception($"Driver Input file format is not correct on the line: {line}");
                }
            }

            return drivers;
        }

        public void CaptureTripInfo(List<string> tripInputLines, List<Driver> drivers)
        {
            foreach (var line in tripInputLines)
            {
                var data = line.Split(' ');

                //To validate if the input feed has exact columns
                if (line.Length != 5)
                {
                    var driverName = data[1].Trim();
                    var startTime = data[2].Trim();
                    var stopTime = data[3].Trim();
                    var milesDriven = data[4].Trim();

                    //Check whether the driver is present in the Driver Entity.
                    var driver = drivers.Where(d => d.Name.Equals(driverName, StringComparison.OrdinalIgnoreCase)
                    ).Select(m => m).FirstOrDefault();

                    if (driver != null)
                    {
                        var tripDetails = new Trip
                        {
                            StartTime = DateTime.ParseExact(startTime, "HH:mm", CultureInfo.InvariantCulture),
                            EndTime = DateTime.ParseExact(stopTime, "HH:mm", CultureInfo.InvariantCulture),
                            MilesDriven = Convert.ToDouble(milesDriven)
                        };

                        _logger.LogDebug($"Trip added for Driver: {driverName}");
                        driver.Trips.Add(tripDetails);
                    }
                    else
                    {
                        _logger.LogError($"Driver {driverName} not found. Trip cannot be added");
                        throw new Exception($"Driver {driverName} not found. Trip cannot be added");
                    }
                }
                else
                {
                    _logger.LogError($"Trip Input file format is not correct on the line: {line}");
                    throw new Exception($"Trip Input file format is not correct on the line: {line}");
                }
            }
        }

        public List<Driver> CalculateDuration(List<Driver> drivers)
        {
            foreach (var driver in drivers)
            {
                if (driver.Trips.Count > 0)
                {
                    foreach (var trip in driver.Trips)
                    {
                        _logger.LogDebug($"Validate Driver {driver.Name} met speed criteria for the Trip");

                        //Validate the Speed Criteria.
                        if (trip.MilesDriven > Constants.MinMiles && trip.MilesDriven < Constants.MaxMiles)
                        {
                            driver.TotalDistance += trip.MilesDriven;
                            driver.TotalTripTime += (trip.EndTime - trip.StartTime).TotalHours;
                        }
                        else
                        {
                            _logger.LogDebug("Trip does not meet speed criteria");
                        }
                    }
                }
            }
            return drivers;
        }

    }
}
