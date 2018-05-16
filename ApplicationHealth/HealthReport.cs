using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationHealth
{
    public class HealthReport
    {
        private readonly IEnumerable<HealthSensor> healthSensors;

        public HealthReport(ApplicationInformation applicationInformation, IEnumerable<HealthSensor> healthSensors)
        {
            this.healthSensors = healthSensors;
            ApplicationInformation = applicationInformation;
        }

        public string OverallResult => healthSensors.Where(s => s.IsEssential).All(s => s.State == HealthState.Ok) ? "CHECK_OK" : "CHECK_FAILED";
        public DateTime Timestamp => DateTime.UtcNow;
        public ApplicationInformation ApplicationInformation { get; private set; }

        public IEnumerable<HealthSensorStatus> HealthSensors
        {
            get
            {
                return healthSensors.Select(s => s.GetStatus());
            }
        }
    }
}
