function convert() {
    let amount = document.getElementById('amount').value;
    let fromCurrency = document.getElementById('fromCurrency').value;
    let toCurrency = document.getElementById('toCurrency').value;
    
    if (amount === '' || isNaN(amount)) {
        document.getElementById('result').innerText = '';
        return;
    }

    // Aquí se deberían usar las tasas de cambio reales
    const exchangeRates = {
        "Colones": 1,
        "Dolares": 0.0018,
        "Euros": 0.0016,
        "Libras": 0.0014,
        "Pesos": 0.041
    };

    let fromRate = exchangeRates[fromCurrency];
    let toRate = exchangeRates[toCurrency];

    let convertedAmount = (amount / fromRate) * toRate;

    document.getElementById('result').innerText = `${amount} ${fromCurrency} son aproximadamente ${convertedAmount.toFixed(2)} ${toCurrency}`;
}
