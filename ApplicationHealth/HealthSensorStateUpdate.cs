using System;

namespace ApplicationHealth
{
    public class HealthSensorStateUpdate
    {
        public string Message { get; set; }
        public HealthState State { get; set; }
        public Exception Exception { get; set; }
    }
}
