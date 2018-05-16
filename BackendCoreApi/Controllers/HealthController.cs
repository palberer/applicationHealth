using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationHealth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendCoreApi.Controllers
{
    [Route("api/Health")]
    public class HealthController : Controller
    {
        private readonly HealthReportProvider healthReportCreator;

        public HealthController(HealthReportProvider healthReportCreator)
        {
            this.healthReportCreator = healthReportCreator;
        }

        [HttpGet("Check")]
        public string Check()
        {
            return "CHECK_OK";
        }

        [HttpGet("Report")]
        public HealthReport Report()
        {
            return healthReportCreator.GetHealthReport();
        }
    }
}