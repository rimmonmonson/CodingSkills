using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SA.Entities;
using SA.Helpers;

namespace SA.Tests
{
    public class DriverUnitTest
    {
        [Fact]
        public void RetrieveDriverInfoTest()
        {
            string driverName = "Rimmon";
            List<string> drivers = new List<string>() { $"Driver {driverName}" };

            var helper = new Helper(new Mock<ILogger<Helper>>().Object);
            var driver = helper.RetrieveDriverInfo(drivers).FirstOrDefault();

            // assert #1 - Driver should not be null
            driver.Should().NotBeNull();
            // assert #2 - Driver Name is same
            driver.Name.Should().Contain(driverName);
        }

        [Fact]
        public void CaptureTripInfo_DriverWithTrip_Test()
        {
            List<Driver> drivers = new List<Driver>()
            {
                new Driver() { Name = "Rimmon" },
                new Driver() { Name = "Monson" }
            };

            List<string> tripInput = new List<string>()
            {
                "Trip Rimmon 07:15 07:45 17.3",
                "Trip Monson 07:15 07:45 17.3",
            };
            var helper = new Helper(new Mock<ILogger<Helper>>().Object);
            helper.CaptureTripInfo(tripInput, drivers);

            foreach (Driver driver in drivers)
            {
                // assert - Driver should have atleast one trip
                driver.Trips.Should().HaveCount(1, "Only 1 Trip included in the trip");
            }

        }

        [Fact]
        public void CaptureTripInfo_DriverWithoutTrip_Test()
        {
            List<Driver> drivers = new List<Driver>()
            {
                new Driver() { Name = "Rimmon" },
                new Driver() { Name = "Monson" }
            };

            List<string> tripInput = new List<string>()
            {
                "Trip Rimmon 07:15 07:45 17.3",
                "Trip Test 07:15 07:45 17.3",
            };
            var helper = new Helper(new Mock<ILogger<Helper>>().Object);
            // assert - If there is an invalid driver added in trip, an exception should be captured.
            Exception ex = Assert.Throws<Exception>(() => helper.CaptureTripInfo(tripInput, drivers));
            Assert.Equal($"Driver Test not found. Trip cannot be added", ex.Message);
        }
    }
}
