using API;
using Newtonsoft.Json;

namespace ExchangeAPI.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ExchangeService(HttpClient httpClient, IConfiguration configuration)
        {
            this._httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, float>> getExchange(string fromCurrency, string toCurrency)
        {
            var apiKey = _configuration["ExchangeAPI:ApiKey"];
            var apiUrl = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{fromCurrency}";

            var response = await _httpClient.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al obtener los datos de la API");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<APIResponse<Dictionary<string, float>>>(jsonResponse);

            if (!apiResponse.Success)
            {
                throw new Exception("La API no devolvió un resultado exitoso");
            }

            if (apiResponse.Data.TryGetValue(toCurrency, out float exchangeRate))
            {
                return apiResponse.Data;
            }
            else
            {
                throw new Exception($"No se encontró la tasa de cambio para {toCurrency}");
            }
        }
    }
}
