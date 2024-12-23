namespace WebApplication1.Dto.Currencies;

public record CurrencyDto(int Id, string Code, string IsoCode, string CurrencyName, string CultureCode, string CurrencySymbol, decimal PercentageIncrease, int DecimalPlaces);
