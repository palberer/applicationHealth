using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ApplicationHealth
{
    public class HealthReportProvider
    {
        private readonly IReadOnlyList<HealthSensor> healthSensors;
        private readonly ApplicationInformation applicationInformation;

        public HealthReportProvider(string applicationName, IReadOnlyList<HealthSensor> healthSensors)
        {
            this.healthSensors = healthSensors;

            var callingAssembly = Assembly.GetCallingAssembly();

            applicationInformation = new ApplicationInformation()
            {
                ApplicationName = applicationName ?? callingAssembly.GetName().Name,
                ApplicationVersion = GetVersion(callingAssembly)
            };
        }

        public HealthReport GetHealthReport()
        {
            var result = new HealthReport(applicationInformation, healthSensors);
            return result;
        }

        private static string GetVersion(Assembly assembly)
        {
            var version = assembly.GetName().Version;
            if (version.MinorRevision == 0)
            {
                try
                {
                    return FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
                }
                catch (Exception)
                {
                    //Should not happened. As ie. process should have file read permissions to own assembly but who knows.
                    //Anyway nothing can be done/logged here.
                }
            }
            return version.ToString();
        }
    }
}
