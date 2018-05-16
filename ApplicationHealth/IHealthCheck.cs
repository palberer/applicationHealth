namespace ApplicationHealth
{
    public interface IHealthCheck
    {
        HealthSensorStateUpdate GetSensorStateUpdate();
    }
}
