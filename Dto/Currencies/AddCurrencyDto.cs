namespace WebApplication1.Dto.Currencies;

public record AddCurrencyDto(string Code, string IsoCode, string CurrencyName, string CultureCode, string CurrencySymbol, decimal PercentageIncrease, int DecimalPlaces);
