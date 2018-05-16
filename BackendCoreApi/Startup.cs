using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationHealth;
using BusinessLogic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BackendCoreApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dataBaseHealthCheck = new DatabaseConnectionCheck("connection-string");

            var sensors = new List<HealthSensor>
            {
                new HealthSensor
                {
                    Name = "LoginProxy",
                    Description = "Proxy to the login service",
                    IsEssential = true,
                    IsExternal = true,
                    KnowledgeBaseArticleUrl = new Uri("http://knowledgebasearticles.support.com/backend-core-api/LoginProxy")
                },
                new HealthSensor
                {
                    Name = "MailScheduler",
                    Description = "Scheduled background activity to send notification emails",
                    IsEssential = true,
                    IsExternal = false,
                    KnowledgeBaseArticleUrl = new Uri("http://knowledgebasearticles.support.com/backend-core-api/MailScheduler")
                },
                new HealthSensor
                {
                    Name = "MyMessageBus",
                    Description = "Main message bus to communicate async with other services",
                    Endpoint = "tcp://tibcoems001.example.com:7222/, tcp://tibcoems002.example.com:7222/",
                    IsEssential = true,
                    IsExternal = true,
                    KnowledgeBaseArticleUrl = new Uri("http://knowledgebasearticles.support.com/backend-core-api/MyMessageBus")
                },
                new HealthSensor
                {
                    Name = "ElasticSearch",
                    Description = "Connection to elastic search cluster",
                    Endpoint = "http://elasticsearch.example.com:8080",
                    IsEssential = true,
                    IsExternal = true,
                    KnowledgeBaseArticleUrl = new Uri("http://knowledgebasearticles.support.com/backend-core-api/ElasticSearch")
                },
                new HealthSensor(activeHealthCheck: dataBaseHealthCheck)
                {
                    Name = "MyDatabase",
                    Description = "Main database connection to load/save application state",
                    Endpoint = "Server=tcp:MyMainDatabase.prod.example.com;Application Name=BackendCoreApi;Integrated Security=SSPI;Database=MyMainDatabase;",
                    IsEssential = true,
                    IsExternal = true,
                    KnowledgeBaseArticleUrl = new Uri("http://knowledgebasearticles.support.com/backend-core-api/MyDatabase")
                },
                 new HealthSensor
                {
                    Name = "ArchiveDatabase",
                    Description = "Archive database connection to load/save historical information",
                    Endpoint = "Server=tcp:ArchiveDatabase.prod.example.com;Application Name=BackendCoreApi;Integrated Security=SSPI;Database=ArchiveDatabase;",
                    IsEssential = false,
                    IsExternal = true,
                    KnowledgeBaseArticleUrl = new Uri("http://knowledgebasearticles.support.com/backend-core-api/ArchiveDatabase")
                }
            };

            var loginHealthSensor = sensors.Single(s => s.Name == "LoginProxy");
            var loginProxy = new LoginServiceProxy("https://loginservice.example.com", loginHealthSensor);

            var healthReportCreator = new HealthReportProvider("backendCoreApi", sensors);

            services.AddMvc();
            services.AddSingleton(healthReportCreator);
            services.AddSingleton(loginProxy);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
        }
    }
}
