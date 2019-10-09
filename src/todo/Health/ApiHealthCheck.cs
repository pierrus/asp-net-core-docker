using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace Todo.Health
{
    public class ApiHealthCheck : IHealthCheck
    {
        private readonly Data.ITodoRepository _todoRepository;

        public ApiHealthCheck(Data.ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                IList<Models.TodoItem> items = _todoRepository.Get();

                var isHealthy = items.Count >= 0;

                if (isHealthy)
                {
                    return Task.FromResult(HealthCheckResult.Healthy("Repository accessible and healthy"));
                }
            }
            catch (Exception e)
            {
                String exceptionDetail = e.Message;
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("Unable to access and read repository"));
        }

        public static Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("status", result.Status.ToString()),
                new JProperty("results", new JObject(result.Entries.Select(pair =>
                new JProperty(pair.Key, new JObject(
                new JProperty("status", pair.Value.Status.ToString()),
                new JProperty("description", pair.Value.Description),
                new JProperty("data", new JObject(pair.Value.Data.Select(
                p => new JProperty(p.Key, p.Value))))))))));

            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented)); 
        }
    }
}