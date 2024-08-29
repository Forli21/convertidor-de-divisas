

async function fetchExchangeRates() {
    const apiKey = '319a303037cec8f2dbd3b74f';  // Reemplaza con tu clave de API
    const apiUrl = `https://v6.exchangerate-api.com/v6/${apiKey}/latest/CRC`; // O la URL de la API que estés usando

    try {
        const response = await fetch(apiUrl);
        const data = await response.json();

        if (data.result !== "success") {
            throw new Error("Error al obtener las tasas de cambio");
        }

        return data.conversion_rates; // Este será un objeto con las tasas de cambio
    } catch (error) {
        console.error("Error al conectar con la API:", error);
        return null;
    }
}

async function convert() {
    let amount = document.getElementById('amount').value;
    let fromCurrency = document.getElementById('fromCurrency').value;
    let toCurrency = document.getElementById('toCurrency').value;

    if (amount === '' || isNaN(amount)) {
        document.getElementById('result').innerText = '';
        return;
    }

    const currencyMap = {
        "Colones": "CRC",
        "Dolares": "USD",
        "Euros": "EUR",
        "Libras": "GBP",
        "Pesos": "MXN"
    };

    let fromCurrencyCode = currencyMap[fromCurrency];
    let toCurrencyCode = currencyMap[toCurrency];

    // Obtén las tasas de cambio desde la API
    let exchangeRates = await fetchExchangeRates();
    if (!exchangeRates) {
        document.getElementById('result').innerText = 'Error obteniendo tasas de cambio';
        return;
    }

    let fromRate = exchangeRates[fromCurrencyCode];
    let toRate = exchangeRates[toCurrencyCode];

    if (fromRate === undefined || toRate === undefined) {
        console.error(`No se pudo encontrar la tasa de cambio para ${fromCurrency} o ${toCurrency}`);
        document.getElementById('result').innerText = 'Error en la conversión, revise las monedas seleccionadas.';
        return;
    }

    let convertedAmount = (amount / fromRate) * toRate;

    const symbolMap = {
        "USD": "$",
        "CRC": "₡",
        "EUR": "€",
        "GBP": "£",
        "MXN": "$"
    };

    let symbolFrom = symbolMap[fromCurrencyCode];
    let symbolTo = symbolMap[toCurrencyCode];

    document.getElementById('result').innerText = `${symbolFrom}${amount} son aproximadamente ${symbolTo}${convertedAmount.toFixed(2)}`;
}
