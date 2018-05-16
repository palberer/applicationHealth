namespace ApplicationHealth
{
    public class HealthSensorStatus
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Endpoint { get; set; }
        public bool IsEssential { get; set; }
        public bool IsExternal { get; set; }
        public string State { get; set; }
        public string Message { get; set; }
        public string KnowledgeBaseArticleUrl { get; set; }
    }
}
