using System;

namespace ApplicationHealth
{
    public class DatabaseConnectionCheck : IHealthCheck
    {
        private readonly string connectionString;

        public DatabaseConnectionCheck(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public HealthSensorStateUpdate GetSensorStateUpdate()
        {
            return new HealthSensorStateUpdate
            {
                State = HealthState.Ok,
                Message = "Successful connect to DB at " + DateTime.UtcNow.ToLongDateString()
            };
        }
    }
}
