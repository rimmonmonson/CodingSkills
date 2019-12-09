using SA.Entities;
using System.Collections.Generic;

namespace SA.Helpers.Interfaces
{
    public interface IHelper
    {
        List<Driver> RetrieveDriverInfo(List<string> driverInputLines);
        void CaptureTripInfo(List<string> tripInputLines, List<Driver> drivers);
        List<Driver> CalculateDuration(List<Driver> drivers);
    }
}