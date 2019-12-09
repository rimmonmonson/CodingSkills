using SA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SA.Helpers.Interfaces
{
    public interface IFileProcessor
    {
        Customer ExtractCustomerDataFromInputFile(string fileName);
    }
}
