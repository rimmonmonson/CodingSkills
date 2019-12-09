using System;
using System.Collections.Generic;

namespace SA.Entities
{
    public class Driver
    {
        public string Name { get; set; }
        public double TotalDistance { get; set; }
        public double TotalTripTime { get; set; }
        public List<Trip> Trips { get; set; } = new List<Trip>();
        public double AverageSpeed
        {
            get
            {
                try
                {
                    if (TotalDistance > 0 && TotalTripTime > 0)
                        return TotalDistance / TotalTripTime;
                    return 0;
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                return;
            }
        }
    }
}
