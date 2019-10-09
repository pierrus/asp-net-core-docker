using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Diagnostics.HealthChecks;

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
                    return Task.FromResult(HealthCheckResult.Healthy("I am one healthy microservice API"));
                }
            }
            catch (Exception e)
            {
                String exceptionDetail = e.Message;
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("I am the sad, unhealthy microservice API"));
        }
    }
}