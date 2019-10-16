using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace todo.health
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            String url = _configuration["Health:Url"];
            _logger.LogInformation(String.Format("Watching URL {0}", url), DateTimeOffset.Now);
            HttpClient client = new HttpClient();


            while (!stoppingToken.IsCancellationRequested)
            {
                try	
                {
                    string responseBody = await client.GetStringAsync(url);

                    _logger.LogInformation("Application healthy", DateTimeOffset.Now);
                }  
                catch(HttpRequestException e)
                {
                    _logger.LogError(e, "API Error", DateTimeOffset.Now);
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
