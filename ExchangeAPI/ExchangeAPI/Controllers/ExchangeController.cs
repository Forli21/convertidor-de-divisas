using Microsoft.AspNetCore.Mvc;

namespace API
{
    [Route("api/exchange-rates")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ExchangeRateController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet("{currency}")]
        public async Task<IActionResult> GetRates(string currency)
        {
            var apiKey = _configuration["ExchangeAPI:ApiKey"];
            var apiUrl = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{currency}";

            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Error al obtener las tasas de cambio.");
                }

                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error al conectar con la API: {ex.Message}");
            }
        }
    }
}
