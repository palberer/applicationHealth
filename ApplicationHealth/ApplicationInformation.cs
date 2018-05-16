using System;

namespace ApplicationHealth
{
    public class ApplicationInformation
    {
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
        public string MachineName => Environment.MachineName;
    }
}
