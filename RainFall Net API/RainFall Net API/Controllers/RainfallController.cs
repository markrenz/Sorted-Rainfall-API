using Microsoft.AspNetCore.Mvc;
using RainFall_Net_API.Models;
using RainFall_Net_API.Responses;

namespace RainfallApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RainfallController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public RainfallController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://environment.data.gov.uk/flood-monitoring/");
        }

        [HttpGet("id/{stationId}/readings")]
        public async Task<IActionResult> GetRainfallReadings(string stationId, int count = 10)
        {
            try
            {
       
                var apiUrl = $"https://environment.data.gov.uk/flood-monitoring/id/stations/{stationId}/readings?_sorted&_limit={count}";

                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var rainfallReadings = await response.Content.ReadFromJsonAsync<RainfallReadingResponse>();

                    return Ok(rainfallReadings);
                }

                return StatusCode((int)response.StatusCode, "Failed to retrieve rainfall readings.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}