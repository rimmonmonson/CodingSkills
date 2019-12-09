using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SA.Helpers.Interfaces;

namespace SA.DriverReportGenerator
{
    public static class Factory
    {
        public static IFileProcessor GetFileProcessor(IServiceProvider services)
        {
            return services.GetRequiredService<IFileProcessor>();
        }

        public static IHelper GetHelper(IServiceProvider services)
        {
            return services.GetRequiredService<IHelper>();
        }

        public static IOutputGenerator GetOutputGenerator(IServiceProvider services)
        {
            return services.GetRequiredService<IOutputGenerator>();
        }
    }
}
