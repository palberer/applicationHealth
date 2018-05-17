using System;

namespace ApplicationHealth
{
    public class HealthSensor : IHealthStatusRecorder
    {
        private readonly IHealthCheck activeHealthCheck;

        public HealthSensor()
        {
            State = HealthState.Unknown;
        }

        public HealthSensor(IHealthCheck activeHealthCheck)
        {
            this.activeHealthCheck = activeHealthCheck;
            State = HealthState.Unknown;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Endpoint { get; set; }
        public bool IsEssential { get; set; }
        public bool IsExternal { get; set; }
        public Uri KnowledgeBaseArticleUrl { get; set; }
        public HealthState State { get; private set; }
        public bool IsHealthy { get { return State != HealthState.Failed; } }
        public Exception Exception { get; private set; }
        public string Message { get; private set; }

        public void SetState(HealthState state)
        {
            State = state;
            Message = "Last success result stored at " + DateTime.UtcNow;
        }

        public void SetState(Exception ex)
        {
            State = HealthState.Failed;
            Message = "Last error result stored at " + DateTime.UtcNow;
        }

        public void SetState(Exception ex, string message)
        {
            State = HealthState.Failed;
            Message = message;
        }

        public HealthSensorStatus GetStatus()
        {
            if (activeHealthCheck != null)
            {
                var updateResult = activeHealthCheck.GetSensorStateUpdate();
                State = updateResult.State;
                Message = updateResult.Message;
                Exception = updateResult.Exception;
            }

            return new HealthSensorStatus
            {
                Name = Name,
                Description = Description,
                Endpoint = Endpoint,
                IsEssential = IsEssential,
                IsExternal = IsExternal,
                KnowledgeBaseArticleUrl = KnowledgeBaseArticleUrl.ToString(),
                State = State.ToString(),
                Message = Message
            };
        }
    }
}
