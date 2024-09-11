public interface IExchangeService
{
    Task<Dictionary<string, float>> getExchange(string fromCurrency, string toCurrency);
}
