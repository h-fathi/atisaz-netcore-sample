using AtisazBazar.DataAccess;
using AtisazBazar.Services;
using AtisazBazar.Services.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace AtisazBazar.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HttpBinController : ControllerBase
    {

        private readonly IDistributedCache _cache; 
        private static readonly SemaphoreSlim semaphore = new(1, 1);
        public HttpBinController(IDistributedCache cache)
        {           
            _cache = cache;
        }


        [Route("Get")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cacheKey = "httpbin_key";
            var responseBody = "";
            // check cache if not exist
            if (!_cache.TryGetValue(cacheKey, out responseBody)) 
            { 
                try
                {
                    await semaphore.WaitAsync();

                    using var httpClient = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://httpbin.org/get");
                    var response = await httpClient.SendAsync(request);
                    using var reader = new StreamReader(response.Content.ReadAsStream());
                    responseBody = await reader.ReadToEndAsync();

                    // cache response
                    var cacheEntryOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(15));
                    await _cache.SetAsync(cacheKey, responseBody, cacheEntryOptions);
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(responseBody);
        }

    }
}