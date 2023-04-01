using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AtisazBazar.WebApp.HealthCheck
{
    public class HttpBinHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {

            try
            {
                using var httpClient = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://httpbin.org/get");
                var response = await httpClient.SendAsync(request);
                using var reader = new StreamReader(response.Content.ReadAsStream());
                var responseBody = await reader.ReadToEndAsync();
                return HealthCheckResult.Healthy("Healthy result from HttpBinHealthCheck");
            }
            
        catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Unhealthy result from HttpBinHealthCheck respnse error is : " + ex.Message);
            }
        }
    }
}
