using System;

namespace ApplicationHealth
{
    public interface IHealthStatusRecorder
    {
        void SetState(HealthState state);
        void SetState(Exception ex);
        void SetState(Exception ex, string message);
    }
}
