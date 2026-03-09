using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MicroondasBennerAPI.Middlewares
{
    public class HealthCheckMiddleware : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = true;

            return isHealthy
                ? Task.FromResult(HealthCheckResult.Healthy())
                : Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus));
        }
    }
}
