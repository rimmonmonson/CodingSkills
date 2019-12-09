using SA.Entities;
using System.Collections.Generic;

namespace SA.Helpers.Interfaces
{
    public interface IOutputGenerator
    {
        void PrintOutput(List<Driver> drivers);
    }
}